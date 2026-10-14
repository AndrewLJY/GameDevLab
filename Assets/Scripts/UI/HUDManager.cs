using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    private Vector3[] scoreTextPosition = {
        new Vector3(-811, 362, 0),
        new Vector3(-203, -8, 0),
        new Vector3(-191, -57, 0),
        };
    private Vector3[] restartButtonPosition = {
        new Vector3(791, 459, 0),
        new Vector3(-27, -143, 0),
        new Vector3(0, -182, 0),
    };

    public GameObject scoreText;
    public GameObject restartButton;

    public GameObject mainGamePanel;
    public GameObject gameOverPanel;
    public GameObject mainMenuPanel;
    public GameObject levelClearPanel;
    public GameObject pauseMenuPanel;

    void Awake()
    {
        GameManager.instance.gameStart.AddListener(GameStart);
        GameManager.instance.gameOver.AddListener(GameOver);
        GameManager.instance.gameRestart.AddListener(GameStart);
        GameManager.instance.scoreChange.AddListener(SetScore);
        GameManager.instance.levelClear.AddListener(LevelClear);
        GameManager.instance.gamePaused.AddListener(PauseGame);
    }

    void Start()
    {
    }

    void Update()
    {

    }

    public void GameStart()
    {
        gameOverPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        levelClearPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);

        mainGamePanel.SetActive(true);
        scoreText.SetActive(true);
        restartButton.SetActive(true);

        scoreText.transform.localPosition = scoreTextPosition[0];
        restartButton.transform.localPosition = restartButtonPosition[0];
    }

    public void GameRestart()
    {
        gameOverPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        levelClearPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);

        mainGamePanel.SetActive(true);
        scoreText.SetActive(true);
        restartButton.SetActive(true);

        scoreText.transform.localPosition = scoreTextPosition[0];
        restartButton.transform.localPosition = restartButtonPosition[0];
    }

    public void SetScore(int score)
    {
        Debug.Log("increasing score");
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }


    public void GameOver()
    {
        mainGamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        scoreText.transform.localPosition = scoreTextPosition[1];
        restartButton.transform.localPosition = restartButtonPosition[1];
    }

    public void LevelClear()
    {
        levelClearPanel.SetActive(true);
        scoreText.transform.localPosition = scoreTextPosition[2];
        restartButton.transform.localPosition = restartButtonPosition[2];
    }

    public void PauseGame()
    {
        if (!(mainMenuPanel.activeSelf || gameOverPanel.activeSelf || levelClearPanel.activeSelf))
        {
            if (Time.timeScale == 0.0f)
            {
                Time.timeScale = 1.0f;
            }
            else
            {
                Time.timeScale = 0.0f;
            }

            pauseMenuPanel.SetActive(!pauseMenuPanel.activeSelf);
        }
    }
}
