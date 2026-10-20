using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public GameConstants gameConstants;

    void Awake()
    {
        //GameManager.instance.gameRestart.AddListener(ResetPlayerHearts);
    }

    public void PlayerTakingDmg()
    {
        transform.GetChild(gameConstants.health).GetComponent<HeartManager>().PlayerTakingDmg();
    }

    public void ResetPlayerHearts()
    {
        Debug.Log("reset player hearts function running");
        foreach (Transform child in transform)
        {
            Debug.Log("restting hearts: " + child.name);
            child.gameObject.SetActive(true);
        }
    }
}
