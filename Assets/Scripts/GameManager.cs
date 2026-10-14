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

    //private int currentScore = 0;
    public IntVariable gameScore;
    //private static bool isGameRestart = false;

    public AudioMixer gameMixer;
    public AudioSource gameAudio;
    public AudioClip bgMusic;

    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;
    public UnityEvent levelClear;
    public UnityEvent gamePaused;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "World-1-1")
        {
            Time.timeScale = 0f;
        }

        SceneManager.activeSceneChanged += SceneSetup;
    }

    public void GameRestart()
    {
        gameScore.Value = 0;
        SetScore(gameScore.Value);
        gameRestart.Invoke();

        ResetBgMusic();

        UnmuteAllAudioSources();

        Time.timeScale = 1.0f;

    }

    public void IncreaseScore(int increment)
    {
        gameScore.ApplyChange(increment);
        SetScore(gameScore.Value);
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

    public void PauseGame()
    {
        gamePaused.Invoke();

        if (gameAudio.isPlaying) gameAudio.Pause();
        else gameAudio.Play();
    }

    public void StartButtonCallback(int input)
    {
        gameStart.Invoke();
        Time.timeScale = 1.0f;
    }

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
        SetScore(gameScore.Value);
    }
}
