using UnityEngine;
using UnityEngine.UIElements;

public class ObstaclesManager : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.gameRestart.AddListener(ResetObstacles);
    }

    public void ResetObstacles()
    {
        foreach (Transform boxContainer in transform)
        {
            foreach (Transform child in boxContainer)
            {
                if (child.name.Contains("Question-Box") || child.name.Contains("Brick"))
                {
                    child.GetComponent<BoxContainerController>().GameRestart();
                }
                else
                {
                    child.GetComponent<CoinController>().GameRestart();
                }
            }
        }
    }
}
