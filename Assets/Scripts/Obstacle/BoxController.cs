using UnityEngine;
using UnityEngine.Rendering;

public class BoxController : MonoBehaviour

{
    private bool isThereCoin = false;
    private GameObject coin = null;
    private Animator boxAnimator;
    private bool hasBeenHit = false;

    void Start()
    {
        boxAnimator = transform.parent.GetComponent<Animator>();

        isThereCoin = transform.parent.Find("Coin");
        if (isThereCoin)
        {
            coin = transform.parent.Find("Coin").gameObject;
        }
    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !hasBeenHit)
        {
            hasBeenHit = true;

            boxAnimator.SetTrigger("onBounce");

            if (isThereCoin && coin.activeSelf)
            {
                Debug.Log("spawn coin");
                coin.GetComponent<CoinController>().SpawnCoin();
            }

        }

    }

    public void ResetBoxHit()
    {
        Debug.Log("Reset");
        hasBeenHit = false;
    }
}
