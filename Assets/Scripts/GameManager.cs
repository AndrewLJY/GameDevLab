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
    //public TextMeshProUGUI mainGameScoreText;
    //public TextMeshProUGUI gameOverScoreText;
    //public TextMeshProUGUI levelClearScoreText;
    //public GameObject MainGameScreen;
    //public GameObject GameOverScreen;
    public GameObject MainMenuScreen;
    //public GameObject LevelClearScreen;

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
            MainMenuScreen.SetActive(false);

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
        // reset score
        currentScore = 0;
        SetScore(currentScore);
        gameRestart.Invoke();

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

    //public void AddScore(int amount)
    //{
    //    currentScore += amount;
    //    UpdateScoreUI();
    //}

    //private void UpdateScoreUI()
    //{
    //    if (mainGameScoreText != null) mainGameScoreText.text = "Score: " + currentScore;
    //    if (gameOverScoreText != null) gameOverScoreText.text = "Score: " + currentScore;
    //    if (levelClearScoreText != null) levelClearScoreText.text = "Score: " + currentScore;
    //}

    public void StartButtonCallback(int input)
    {
        //MainMenuScreen.SetActive(false);

        //Time.timeScale = 1.0f;

        gameStart.Invoke();
        Time.timeScale = 1.0f;
    }

    public void RestartButtonCallback(int input)
    {
        Time.timeScale = 1.0f;

        isGameRestart = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Keep just in case
    //private void ResetGame()
    //{
    //    // reset position
    //    mario.GetComponent<Transform>().position = new Vector3(-0.947f, -0.292f, 0.0f);
    //    mario.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);

    //    // reset sprite direction
    //    playerController.faceRightState = true;
    //    playerController.playerHealth = 3;

    //    // reset Goomba
    //    foreach (Transform enemy in enemies.transform)
    //    {
    //        enemy.transform.localPosition = enemy.GetComponent<EnemyController>().startPosition;
    //        enemy.GetComponent<EnemyController>().enemyHealth = 3;
    //    }

    //    // reset score
    //    currentScore = 0;
    //    UpdateScoreUI();

    //    foreach (Transform heart in playerHearts.transform)
    //    {
    //        heart.gameObject.SetActive(true);
    //    }

    //    MainGameScreen.SetActive(true);
    //    GameOverScreen.SetActive(false);
    //}

    public void LevelClear()
    {

        levelClear.Invoke();
        Time.timeScale = 0.0f;
    }
}
