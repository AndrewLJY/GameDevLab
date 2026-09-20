using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int currentScore = 0;
    public GameObject mario;
    public PlayerMovement playerMovement;
    public TextMeshProUGUI mainGameScoreText;
    public TextMeshProUGUI gameOverScoreText;
    public GameObject MainGameScreen;
    public GameObject GameOverScreen;
    public GameObject enemies;
    public GameObject playerHearts;

    void Awake()
    {
        // Ensure there is only ever one manager
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
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
    }

    public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart!");
        // reset everything
        //ResetGame();

        // resume time
        Time.timeScale = 1.0f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ResetGame()
    {
        // reset position
        mario.GetComponent<Transform>().position = new Vector3(-0.947f, -0.292f, 0.0f);
        mario.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);

        // reset sprite direction
        playerMovement.faceRightState = true;
        playerMovement.playerHealth = 3;

        // reset Goomba
        foreach (Transform enemy in enemies.transform)
        {
            enemy.transform.localPosition = enemy.GetComponent<EnemyMovement>().startPosition;
            enemy.GetComponent<EnemyMovement>().enemyHealth = 3;
        }

        // reset score
        currentScore = 0;
        UpdateScoreUI();

        foreach (Transform heart in playerHearts.transform)
        {
            heart.gameObject.SetActive(true);
        }

        MainGameScreen.SetActive(true);
        GameOverScreen.SetActive(false);
    }
}
