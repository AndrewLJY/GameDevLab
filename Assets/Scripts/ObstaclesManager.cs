using UnityEngine;
using UnityEngine.UIElements;

public class ObstaclesManager : MonoBehaviour
{
    public void ResetObstacles()
    {
        foreach (Transform boxContainer in transform)
        {
            foreach (Transform child in boxContainer)
            {
                if (child.name == "Question-Box" || child.name == "Brick")
                {
                    child.GetComponent<BoxController>().GameRestart();
                }
                else
                {
                    child.GetComponent<CoinController>().GameRestart();
                }
            }
        }
    }
}
