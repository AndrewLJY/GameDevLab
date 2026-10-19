using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [System.NonSerialized] public Animator marioAnimator;
    [System.NonSerialized] public bool isInvulnerable = false;

    [System.NonSerialized] public bool faceRightState = true;
    //[System.NonSerialized] public int gameConstants.health = 3;

    public GameConstants gameConstants;

    private bool onGroundState = true;
    private bool isMoving = false;
    private bool jumpedState = false;

    private Rigidbody2D marioBody;
    //public AudioClip marioDeath;
    public AudioClip marioDmg;
    public AudioSource marioAudio;
    public AudioSource marioDeathAudio;
    public GameObject playerHearts;
    public MarioActions marioActions;

    GameManager gameManager;

    public UnityEvent gameOver;

    public BoolVariable marioFaceRight;

    void Awake()
    {
        //GameManager.instance.gameRestart.AddListener(GameRestart);
        marioBody = GetComponent<Rigidbody2D>();
        marioAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        //gameManager = GameManager.instance;

        SetStartingPosition();

        Application.targetFrameRate = 30;

    }

    void Update()
    {
    }

    void Move(int value)
    {
        Vector2 movement = new Vector2(value, 0);
        marioAnimator.SetBool("isWalking", isMoving);
        if (marioBody.linearVelocity.magnitude < gameConstants.maxSpeed)

            marioBody.AddForce(movement * gameConstants.speed);
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
        if (gameConstants.health > 0 && onGroundState)
        {
            marioBody.AddForce(Vector2.up * gameConstants.upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            marioAudio.PlayOneShot(marioAudio.clip);
            marioAnimator.SetBool("isJumping", true);

        }
    }

    public void JumpHold()
    {
        if (gameConstants.health > 0 && jumpedState)
        {
            marioBody.AddForce(Vector2.up * gameConstants.upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;

        }
    }

    void FlipMarioSprite(int value)
    {
        Vector3 currentScale = transform.localScale;
        
        if (value == -1 && faceRightState)
        {
            updateMarioShouldFaceRight(false);
            faceRightState = false;
            currentScale.x = value;
            transform.localScale = currentScale;

        }

        else if (value == 1 && !faceRightState)
        {
            updateMarioShouldFaceRight(true);
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
            
            if (gameConstants.health > 0 && isMoving)
            {
                Move(faceRightState == true ? 1 : -1);
            }

        }
    }

    public void ApplyKnockback(Vector2 direction)
    {
        isInvulnerable = true;
        marioBody.linearVelocity = Vector2.zero;
        marioBody.AddForce(direction * gameConstants.enemyKnockbackForce, ForceMode2D.Impulse);
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

            gameConstants.health -= 1;

            playerHearts.transform.GetChild(gameConstants.health).GetComponent<HeartManager>().PlayerTakingDmg();

            if (gameConstants.health == 1)
            {
                //gameManager.SpeedUpBgMusic(1.3f);
            }

            if (gameConstants.health <= 0)
            {
                //gameManager.SpeedUpBgMusic(1.0f);
                marioAudio.PlayOneShot(gameConstants.deathMusic);
                Time.timeScale = 0.0f;
                gameOver.Invoke();
            }
        }

        isInvulnerable = false;
    }

    public void GameRestart()
    {
        SetStartingPosition();

        marioBody.transform.localScale = new Vector3(1f, 1f, 1f);
        marioBody.linearVelocity = Vector2.zero;

        faceRightState = true;
        marioAnimator.Play("Idle");
        gameConstants.health = 3;
        isInvulnerable = false;
    }

    public void SetStartingPosition()
    {
        Scene current = SceneManager.GetActiveScene();

        if (current.name == "World-1-2")
        {
            this.transform.position = new Vector3(-1.947f, -0.318f, 0.0f);
            gameConstants.health = 3;
        }
        else
        {
            this.transform.position = gameConstants.marioStartingPosition;
        }
    }

    public void PauseGame()
    {
        gameManager.PauseGame();
    }

    private void updateMarioShouldFaceRight(bool value)
    {
        faceRightState = value;
        marioFaceRight.SetValue(faceRightState);
    }
}