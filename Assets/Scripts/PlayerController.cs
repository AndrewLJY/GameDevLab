using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private bool isMoving = false;
    private bool jumpedState = false;

    private SpriteRenderer marioSprite;
    private Rigidbody2D marioBody;
    public AudioClip marioDeath;
    public AudioClip marioDmg;
    public AudioSource marioAudio;
    public GameObject playerHearts;
    public MarioActions marioActions;

    void Start()
    {
        marioSprite = GetComponent<SpriteRenderer>();

        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A) && faceRightState)
        //{
        //    faceRightState = false;
        //    Flip();
        //}

        //if (Input.GetKeyDown(KeyCode.D) && !faceRightState)
        //{
        //    faceRightState = true;
        //    Flip();
        //}
    }

    //void Flip()
    //{

    //    Vector3 currentScale = transform.localScale;
    //    currentScale.x *= -1;
    //    transform.localScale = currentScale;
    //}

    void Move(int value)
    {

        Vector2 movement = new Vector2(value, 0);
        animator.SetBool("isWalking", isMoving);
        // check if it doesn't go beyond maxSpeed
        if (marioBody.linearVelocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed);
    }

    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            isMoving = false;
            animator.SetBool("isWalking", isMoving);

        }
        else
        {
            FlipMarioSprite(value);
            isMoving = true;
            animator.SetBool("isWalking", isMoving);

            Move(value);
        }
    }

    public void Jump()
    {
        if (playerHealth > 0 && onGroundState)
        {
            // jump
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            marioAudio.PlayOneShot(marioAudio.clip);
            // update animator state
            animator.SetBool("isJumping", true);

        }
    }

    public void JumpHold()
    {
        if (playerHealth > 0 && jumpedState)
        {
            // jump higher
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
            //  marioSprite.flipX = true;
            //  if (marioBody.linearVelocity.x > 0.05f)
            //    animator.SetTrigger("onSkid");

        }

        else if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            currentScale.x = value;
            transform.localScale = currentScale;
            //  marioSprite.flipX = false;
            //  if (marioBody.linearVelocity.x < -0.05f)
            //    animator.SetTrigger("onSkid");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacles"))
        {
            animator.SetBool("isJumping", false);
            onGroundState = true;
        }
    }

    void FixedUpdate()
    {
        if (!isInvulnerable)
        {
            //float moveHorizontal = Input.GetAxisRaw("Horizontal");
            
            if (playerHealth > 0 && isMoving)
            {
                Move(faceRightState == true ? 1 : -1);
            }

            //if (isMoving)
            //{
            //    Vector2 movement = new Vector2(moveHorizontal, 0);
            //    if (marioBody.linearVelocity.magnitude < maxSpeed)
            //        marioBody.AddForce(movement * speed);
            //}

            //if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            //{
            //    marioBody.linearVelocity = Vector2.zero;
            //}

            //if (Input.GetKeyDown(KeyCode.Space) && onGroundState)
            //{
            //    animator.SetBool("isJumping", true);
            //    marioAudio.PlayOneShot(marioAudio.clip);
            //    marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            //    onGroundState = false;
            //}
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
                MuteAllAndPlayDeath();
                Time.timeScale = 0.0f;
                GameManager.Instance.MainGameScreen.SetActive(false);
                GameManager.Instance.GameOverScreen.SetActive(true);
            }
        }

        isInvulnerable = false;
    }

    private void MuteAllAndPlayDeath()
    {
        AudioSource[] allAudioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (AudioSource source in allAudioSources)
        {
            if (source != marioAudio)
            {
                source.mute = true;
            }
        }

        marioAudio.mute = false;
        marioAudio.PlayOneShot(marioDeath);

    }
}