using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [System.NonSerialized]
    public PlayerController playerController;

    public GameObject mario;
    public TextMeshProUGUI mainGameScoreText;
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI levelClearScoreText;
    public GameObject MainGameScreen;
    public GameObject GameOverScreen;
    public GameObject MainMenuScreen; 
    public GameObject LevelClearScreen;

    private int currentScore = 0;
    private static bool isGameRestart = false;

    public AudioSource gameAudio;
    public AudioClip bgMusic;
    public GameObject enemies;
    public GameObject playerHearts;

    void Awake()
    {
        // Ensure there is only ever one manager
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (isGameRestart)
        {
            MainMenuScreen.SetActive(false);

            Time.timeScale = 1.0f;

            // Reset the variable so future fresh boots work normally
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

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        // Updates both screens simultaneously 
        if (mainGameScoreText != null) mainGameScoreText.text = "Score: " + currentScore;
        if (gameOverScoreText != null) gameOverScoreText.text = "Score: " + currentScore;
        if (levelClearScoreText != null) levelClearScoreText.text = "Score: " + currentScore;
    }

    public void StartButtonCallback(int input)
    {
        MainMenuScreen.SetActive(false);

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
        Time.timeScale = 0f;
        LevelClearScreen.SetActive(true);
    }
}
