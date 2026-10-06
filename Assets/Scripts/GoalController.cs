using TMPro;
using UnityEngine;

public class GoalController : MonoBehaviour
{

    private bool isPlayerInside = false;

    public AudioSource goalAudio;
    public AudioClip levelCleared;

    void ToggleGoalInstruction(bool tog)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(tog);
        isPlayerInside = tog;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            ToggleGoalInstruction(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            ToggleGoalInstruction(false);
        }
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.F))
        {
            goalAudio.PlayOneShot(goalAudio.clip);
            GameManager.Instance.LevelClear();
        }
    }

}
