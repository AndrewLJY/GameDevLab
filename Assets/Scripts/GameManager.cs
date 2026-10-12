using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : Singleton<GameManager>
{
    [System.NonSerialized]
    public PlayerController playerController;

    private int currentScore = 0;
    private static bool isGameRestart = false;

    public AudioMixer gameMixer;
    public AudioSource gameAudio;
    public AudioClip bgMusic;

    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;
    public UnityEvent levelClear;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "World-1-1")
        {
            Time.timeScale = 0f;
        }

        gameAudio.PlayOneShot(bgMusic);
        SceneManager.activeSceneChanged += SceneSetup;
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
        gameAudio.pitch = speedMultiplier;

        float correctedPitch = 1f / speedMultiplier;

        gameMixer.SetFloat("MyPitchParam", correctedPitch);
    }

    public void SceneSetup(Scene current, Scene next)
    {
        gameStart.Invoke();
        SetScore(currentScore);
    }
}
