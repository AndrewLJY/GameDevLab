using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public void ResetPlayerHearts()
    {
        foreach (GameObject child in transform)
        {
            Debug.Log(gameObject);
            child.SetActive(true);
        }
    }
}
