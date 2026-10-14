using UnityEngine;
using UnityEngine.Rendering;

public class BoxContainerController : MonoBehaviour

{
    private bool isThereCoin = false;
    private bool isQuestionBox = false;
    private GameObject coin = null;
    private Animator boxAnimator;
    private RigidbodyType2D boxRigidbodyType2D;


    void Start()
    {
        isQuestionBox = gameObject.transform.GetChild(0).name == "Question-Box";

        
        boxAnimator = transform.GetComponent<Animator>();
        

        isThereCoin = transform.Find("Coin");
        if (isThereCoin)
        {
            coin = transform.Find("Coin").gameObject;
        }

        boxRigidbodyType2D = gameObject.GetComponent<Rigidbody2D>().bodyType;

    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        boxAnimator.SetTrigger("onBounce");

        if (isThereCoin && coin.activeSelf)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                coin.GetComponent<CoinController>().SpawnCoin();
            }
        }
    }

    public void DisableBox()
    {
        if (isQuestionBox) boxAnimator.SetBool("boxDisabled", true);
        boxRigidbodyType2D = RigidbodyType2D.Static;
    }

    public void GameRestart()
    {
        if (isQuestionBox) boxAnimator.SetBool("boxDisabled", false);
        boxRigidbodyType2D = RigidbodyType2D.Dynamic;
    }
}
