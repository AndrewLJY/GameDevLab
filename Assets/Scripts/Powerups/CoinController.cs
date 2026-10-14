using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinController : MonoBehaviour
{
    private Animator coinAnimator;
    public AudioSource coinAudio;

    public int parameter;
    public UnityEvent<int> useInt;

    void Start()
    {
        coinAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void SpawnCoin()
    {
        coinAnimator.SetTrigger("spawnCoin");
        if(!coinAudio.isPlaying) coinAudio.PlayOneShot(coinAudio.clip);
    }

    public void CoinTaken()
    {
        gameObject.SetActive(false);
    }

    public void TriggerIntEvent()
    {
        useInt.Invoke(parameter);
    }

    public void GameRestart()
    {
        gameObject.SetActive(true);
    }
}
