using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinController : MonoBehaviour
{
    private Animator coinAnimator;
    public AudioSource coinAudio;

    public int scoreValue = 1;
    public UnityEvent<int> increaseScore;

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
        increaseScore.Invoke(scoreValue);
    }

    public void GameRestart()
    {
        gameObject.SetActive(true);
    }
}
