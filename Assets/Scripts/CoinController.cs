using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinController : MonoBehaviour
{
    private Animator animator;
    public AudioSource coinAudio;
    public GameObject box;

    public int parameter;
    public UnityEvent<int> useInt;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void SpawnCoin()
    {
        animator.SetTrigger("spawnCoin");
        coinAudio.PlayOneShot(coinAudio.clip);
    }

    public void CoinTaken()
    {
        gameObject.SetActive(false);
        if (box.name == "Question-Box")
        {
            box.GetComponent<BoxController>().DisableBox();
        }
    }

    public void TriggerIntEvent()
    {
        Debug.Log("trigger int event");
        useInt.Invoke(parameter);
    }

    public void GameRestart()
    {
        gameObject.SetActive(true);
    }
}
