using UnityEngine;

public class HealthManager : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.gameRestart.AddListener(ResetPlayerHearts);
    }

    public void ResetPlayerHearts()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
