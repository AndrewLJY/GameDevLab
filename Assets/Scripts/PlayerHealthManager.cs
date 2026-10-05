using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public void ResetPlayerHearts()
    {
        foreach (Transform child in transform)
        {
            Debug.Log("player heart");
            child.gameObject.SetActive(true);
        }
    }
}
