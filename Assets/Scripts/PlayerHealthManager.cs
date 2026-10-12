using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.gameRestart.AddListener(ResetPlayerHearts);
    }

    public void ResetPlayerHearts()
    {
        foreach (Transform child in transform)
        {
            Debug.Log("player heart");
            child.gameObject.SetActive(true);
        }
    }
}
