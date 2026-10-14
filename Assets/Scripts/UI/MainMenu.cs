using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour
{
    public GameObject highScoreText;
    public IntVariable highScore;

    void Start()
    {
        SetHighScore();
    }

    public void GoToLoadingScene()
    {
        SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Single);
    }

    void SetHighScore()
    {
        highScoreText.GetComponent<TextMeshProUGUI>().text = "TOP- " + highScore.previousHighestValue.ToString("D6");
    }

    public void ResetHighScore()
    {
        GameObject eventSystem = GameObject.Find("EventSystem");
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);

        highScore.ResetHighestValue();
        SetHighScore();
    }
}
