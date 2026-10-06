using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [System.NonSerialized]
    public PlayerController playerController;

    public GameObject mario;

    private int currentScore = 0;
    private static bool isGameRestart = false;

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

        if (isGameRestart)
        {
            Time.timeScale = 1.0f;

            isGameRestart = false;
        }
        else
        {
            Time.timeScale = 0f;
        }
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

        InterruptAudioSources();

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

    public void RestartButtonCallback(int input)
    {
        Time.timeScale = 1.0f;

        isGameRestart = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LevelClear()
    {

        levelClear.Invoke();
        Time.timeScale = 0.0f;
    }

    void InterruptAudioSources()
    {
        AudioSource[] allAudioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audio in allAudioSources)
        {
            audio.Stop();
        }
    }

    void UnmuteAllAudioSources()
    {
        AudioSource[] allAudioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audio in allAudioSources)
        {
            audio.mute = false;
        }
    }
}
