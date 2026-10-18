using UnityEngine;

public class HealthManager : MonoBehaviour
{
    void Awake()
    {
        //GameManager.instance.gameRestart.AddListener(ResetPlayerHearts);
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
