using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public AudioManager AudioManager;

    [System.NonSerialized]
    public PlayerController playerController;

    public GameObject mario;

    private int currentScore = 0;
    private static bool isGameRestart = false;

    public AudioMixer gameMixer;
    public AudioSource gameAudio;
    public AudioClip bgMusic;
    public GameObject enemies;
    public GameObject playerHearts;

    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;
    public UnityEvent levelClear;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Time.timeScale = 0f;
    }

    void Start()
    {
        gameAudio.PlayOneShot(bgMusic);
    }

    public void GameRestart()
    {
        currentScore = 0;
        SetScore(currentScore);
        gameRestart.Invoke();

        ResetBgMusic();

        UnmuteAllAudioSources();

        Time.timeScale = 1.0f;

    }

    public void IncreaseScore(int increment)
    {
        currentScore += increment;
        SetScore(currentScore);
    }

    public void SetScore(int score)
    {
        scoreChange.Invoke(score);
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOver.Invoke();
    }

    public void StartButtonCallback(int input)
    {
        gameStart.Invoke();
        Time.timeScale = 1.0f;
    }

    //public void RestartButtonCallback(int input)
    //{
    //    Time.timeScale = 1.0f;

    //    isGameRestart = true;

    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}

    public void LevelClear()
    {
        levelClear.Invoke();
        Time.timeScale = 0.0f;
    }

    void UnmuteAllAudioSources()
    {
        AudioSource[] allAudioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audio in allAudioSources)
        {
            audio.mute = false;
        }
    }

    void ResetBgMusic()
    {   
        if (gameAudio != null)
        {
            gameAudio.mute = false;

            gameAudio.Stop();

            if (gameAudio.clip != null)
            {
                gameAudio.time = 0f;
            }

            gameAudio.Play();
        }
    }

    public void SpeedUpBgMusic(float speedMultiplier)
    {
        // 1. Speed up the physical playback of the clip
        gameAudio.pitch = speedMultiplier;

        // 2. Invert the pitch shift in the mixer to keep the tone normal
        // If speed is 1.3f, mixer pitch needs to be 1 / 1.3f = 0.77f
        float correctedPitch = 1f / speedMultiplier;

        // Ensure "MyPitchParam" is exposed in your Audio Mixer
        gameMixer.SetFloat("MyPitchParam", correctedPitch);
    }
}
