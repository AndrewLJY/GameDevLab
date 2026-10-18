using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class BoxContainerController : MonoBehaviour

{
    private bool isQuestionBox = false;
    private Animator boxAnimator;
    //private RigidbodyType2D boxRigidbodyType2D;

    public UnityEvent powerupReset;


    void Start()
    {
        isQuestionBox = gameObject.transform.GetChild(0).name.Contains("Question-Box");

        boxAnimator = transform.GetComponent<Animator>();
    
        //boxRigidbodyType2D = gameObject.GetComponent<Rigidbody2D>().bodyType;

    }

    void Update()
    {
    }

    public void DisableBox()
    {
        if (isQuestionBox) boxAnimator.SetBool("boxDisabled", true);
        gameObject.transform.GetChild(0).GetComponent<BoxController>().ResetBoxHit();

    }

    public void GameRestart()
    {
        if (isQuestionBox) boxAnimator.SetBool("boxDisabled", false);
        powerupReset.Invoke();

    }
}
