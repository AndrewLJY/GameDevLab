using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.NonSerialized] public Animator animator;
    [System.NonSerialized] public bool isInvulnerable = false;

    [System.NonSerialized] public bool faceRightState = true;
    [System.NonSerialized] public int playerHealth = 3;
    public float speed = 7;
    public float maxSpeed = 15;
    public float upSpeed = 5;
    private bool onGroundState = true;

    public AudioClip marioDeath;
    public AudioClip marioDmg;
    public AudioSource marioAudio;
    private SpriteRenderer marioSprite;
    private Rigidbody2D marioBody;
    public GameObject playerHearts;

    void Start()
    {
        marioSprite = GetComponent<SpriteRenderer>();

        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && faceRightState)
        {
            faceRightState = false;
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.D) && !faceRightState)
        {
            faceRightState = true;
            Flip();
        }
    }

    void Flip()
    {

        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) 
        {
            animator.SetBool("isJumping", false);
            onGroundState = true;
        }
    }

    void FixedUpdate()
    {
        if (!isInvulnerable)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            bool isWalking = Mathf.Abs(moveHorizontal) > 0.3f;
            animator.SetBool("isWalking", isWalking);

            if (isWalking)
            {
                Vector2 movement = new Vector2(moveHorizontal, 0);
                if (marioBody.linearVelocity.magnitude < maxSpeed)
                    marioBody.AddForce(movement * speed);
            }

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                marioBody.linearVelocity = Vector2.zero;
            }

            if (Input.GetKeyDown(KeyCode.Space) && onGroundState)
            {
                animator.SetBool("isJumping", true);
                marioAudio.PlayOneShot(marioAudio.clip);
                marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
                onGroundState = false;
            }
        }
    }

    public void ApplyKnockback(Vector2 direction)
    {
        isInvulnerable = true;
        marioBody.linearVelocity = Vector2.zero;
        marioBody.AddForce(direction * 4f, ForceMode2D.Impulse);
    }

    public void TakeDamage(Collider2D other)
    {
        if (!isInvulnerable)
        {
            isInvulnerable = true;

            marioAudio.PlayOneShot(marioDmg);
            animator.SetTrigger("TakingDmg");
            Vector2 heading = transform.position - other.transform.position;
            Vector2 knockbackDirection = new Vector2(Mathf.Sign(heading.x), 0.3f).normalized;
            ApplyKnockback(knockbackDirection);

            playerHealth -= 1;

            playerHearts.transform.GetChild(playerHealth).GetComponent<HealthManager>().PlayerTakingDmg();

            if (playerHealth <= 0)
            {
                marioAudio.PlayOneShot(marioDeath);
                Time.timeScale = 0.0f;
                GameManager.Instance.MainGameScreen.SetActive(false);
                GameManager.Instance.GameOverScreen.SetActive(true);
            }
        }

        isInvulnerable = false;
    }
}