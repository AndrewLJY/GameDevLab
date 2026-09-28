using UnityEngine;

public class CoinController : MonoBehaviour
{
    private Animator animator;
    public AudioSource coinAudio;
    public GameObject box;

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
        if(box.name == "Question-Box")
        {
            box.GetComponent<BoxController>().DisableBox();
        }
    }
}
