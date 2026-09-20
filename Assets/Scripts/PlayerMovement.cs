using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer marioSprite;
    public bool faceRightState = true;
    public float speed = 10;
    public float maxSpeed = 20;
    public int playerHealth = 3;
    //private float knockbackTimer = 0f;
    public bool isInvulnerable = false;
    private Rigidbody2D marioBody;
    public float upSpeed = 10;
    private bool onGroundState = true;
    public GameManager gameManager;
    public GameObject playerHearts;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        marioSprite = GetComponent<SpriteRenderer>();

        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            Flip();
        }

        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            
            Flip();
        }
    }

    void Flip()
    {

        // Multiply the player's x local scale by -1
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }

    // FixedUpdate is called 50 times a second
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        bool isWalking = Mathf.Abs(moveHorizontal) > 0.3f;
        animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            // check if it doesn't go beyond maxSpeed
            if (marioBody.linearVelocity.magnitude < maxSpeed)
                marioBody.AddForce(movement * speed);
        }

        // stop
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            // stop
            marioBody.linearVelocity = Vector2.zero;
        }

        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
        }
    }

    public void ApplyKnockback(Vector2 direction)
    {
        isInvulnerable = true;
        //knockbackTimer = 1f;
        marioBody.linearVelocity = Vector2.zero;
        marioBody.AddForce(direction * 4f, ForceMode2D.Impulse);
    }

    public void TakeDamage(Collider2D other)
    {
        if (!isInvulnerable)
        {
            Debug.Log("Collided with goomba!");
            isInvulnerable = true;

            animator.SetTrigger("TakingDmg");
            Vector2 heading = transform.position - other.transform.position;
            Vector2 knockbackDirection = new Vector2(Mathf.Sign(heading.x), 0.3f).normalized;
            ApplyKnockback(knockbackDirection);

            playerHealth -= 1;

            playerHearts.transform.GetChild(playerHealth).GetComponent<HealthManager>().PlayerTakingDmg();

            if (playerHealth <= 0)
            {
                Time.timeScale = 0.0f;
                GameManager.Instance.MainGameScreen.SetActive(false);
                GameManager.Instance.GameOverScreen.SetActive(true);
            }
        }

        isInvulnerable = false;
    }
}