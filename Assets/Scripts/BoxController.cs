using UnityEngine;
using UnityEngine.Rendering;

public class BoxController : MonoBehaviour

{
    private bool isThereCoin = false;
    private bool isQuestionBox = false;
    private GameObject coin = null;
    private Animator animator;

    void Start()
    {
        isThereCoin = transform.parent.Find("Coin");
        if (isThereCoin)
        {
            coin = transform.parent.Find("Coin").gameObject;
        }
        isQuestionBox = gameObject.name == "Question-Box";

        if (isQuestionBox)
        {
            animator = transform.parent.GetComponent<Animator>();
        }

    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (isThereCoin && coin.activeSelf)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                coin.GetComponent<CoinController>().SpawnCoin();
                if (isQuestionBox)
                {
                    animator.SetBool("boxDisabled", true);
                }
            }
        }
    }

    public void DisableBox()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    public void GameRestart()
    {
        if (gameObject.name == "Question-Box") animator.SetBool("boxDisabled", false);
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
