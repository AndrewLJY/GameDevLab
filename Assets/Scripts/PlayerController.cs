using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [System.NonSerialized] public Animator marioAnimator;
    [System.NonSerialized] public bool isInvulnerable = false;

    [System.NonSerialized] public bool faceRightState = true;
    [System.NonSerialized] public int playerHealth = 3;
    public float speed = 7;
    public float maxSpeed = 15;
    public float upSpeed = 5;
    private bool onGroundState = true;
    private bool isMoving = false;
    private bool jumpedState = false;

    private SpriteRenderer marioSprite;
    private Rigidbody2D marioBody;
    public AudioClip marioDeath;
    public AudioClip marioDmg;
    public AudioSource marioAudio;
    public AudioSource marioDeathAudio;
    public GameObject playerHearts;
    public MarioActions marioActions;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();

        marioSprite = GetComponent<SpriteRenderer>();

        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioAnimator = GetComponent<Animator>();

    }

    void Update()
    {
    }

    void Move(int value)
    {

        Vector2 movement = new Vector2(value, 0);
        marioAnimator.SetBool("isWalking", isMoving);
        if (marioBody.linearVelocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed);
    }

    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            isMoving = false;
            marioAnimator.SetBool("isWalking", isMoving);

        }
        else
        {
            FlipMarioSprite(value);
            isMoving = true;
            marioAnimator.SetBool("isWalking", isMoving);

            Move(value);
        }
    }

    public void Jump()
    {
        if (playerHealth > 0 && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            marioAudio.PlayOneShot(marioAudio.clip);
            marioAnimator.SetBool("isJumping", true);

        }
    }

    public void JumpHold()
    {
        if (playerHealth > 0 && jumpedState)
        {
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;

        }
    }

    void FlipMarioSprite(int value)
    {
        Vector3 currentScale = transform.localScale;
        
        if (value == -1 && faceRightState)
        {
            faceRightState = false;
            currentScale.x = value;
            transform.localScale = currentScale;

        }

        else if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            currentScale.x = value;
            transform.localScale = currentScale;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacles"))
        {
            marioAnimator.SetBool("isJumping", false);
            onGroundState = true;
        }
    }

    void FixedUpdate()
    {
        if (!isInvulnerable)
        {
            
            if (playerHealth > 0 && isMoving)
            {
                Move(faceRightState == true ? 1 : -1);
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
            marioAnimator.SetTrigger("TakingDmg");
            Vector2 heading = transform.position - other.transform.position;
            Vector2 knockbackDirection = new Vector2(Mathf.Sign(heading.x), 0.3f).normalized;
            ApplyKnockback(knockbackDirection);

            playerHealth -= 1;

            playerHearts.transform.GetChild(playerHealth).GetComponent<HealthManager>().PlayerTakingDmg();

            if (playerHealth == 1)
            {
                gameManager.SpeedUpBgMusic(1.3f);
            }

            if (playerHealth <= 0)
            {
                gameManager.SpeedUpBgMusic(1.0f);
                marioDeathAudio.PlayOneShot(marioDeathAudio.clip);
                Time.timeScale = 0.0f;
                gameManager.GameOver();
            }
        }

        isInvulnerable = false;
    }

    public void GameRestart()
    {
        marioBody.transform.position = new Vector3(-0.947f, -0.318f, 0.0f);
        marioBody.transform.localScale = new Vector3(1f, 1f, 1f);
        marioBody.linearVelocity = Vector2.zero;

        faceRightState = true;
        marioAnimator.Play("Idle");
        playerHealth = 3;
        isInvulnerable = false;
    }

    public void GameRestart()
    {
        // reset position
        marioBody.transform.position = new Vector3(-0.947f, -0.318f, 0.0f);
        
        // reset sprite direction
        faceRightState = true;
        //marioSprite.flipX = false;

        // reset animation
        animator.SetTrigger("gameRestart");
        playerHealth = 3;
        isInvulnerable = false;

        // reset camera position
        //gameCamera.position = new Vector3(0, 0, -10);
    }
}