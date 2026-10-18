using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstaclesManager : MonoBehaviour
{
    void Awake()
    {
        //GameManager.instance.gameRestart.AddListener(ResetObstacles);
    }

    public void ResetObstacles()
    {
        GameObject[] BoxObstacles = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None)
                                     .Where(obj => obj.name.Contains("Question-Box") || obj.name.Contains("Brick"))
                                     .ToArray();

        GameObject[] Coins = Resources.FindObjectsOfTypeAll<GameObject>()
                                     .Where(obj => obj.name == "Coin" && obj.hideFlags == HideFlags.None)
                                     .ToArray();

        foreach (GameObject child in BoxObstacles)
        {
            if(child.transform.GetComponent<BoxContainerController>() != null)
            { 
                child.transform.GetComponent<BoxContainerController>().GameRestart(); 
            }
        }

        foreach (GameObject child in Coins)
        {
            child.GetComponent<CoinController>().GameRestart();
        }
        
    }
}
