using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    public Animator animator;
    private float originalX;
    private float moveSpeed = 0.5f;
    public int enemyHealth = 3;
    public bool isInvulnerable = false;
    private Rigidbody2D enemyBody;
    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f); 

    private float knockbackTimer = 0f;
    public GameManager gameManager;
    private Transform playerTransform;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    public void ApplyKnockback(Vector2 direction, float strength)
    {
        isInvulnerable = true;
        knockbackTimer = 1f;
        enemyBody.linearVelocity = Vector2.zero;
        enemyBody.AddForce(direction * strength, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        if (knockbackTimer > 0)
        {
            knockbackTimer -= Time.fixedDeltaTime;
            if (knockbackTimer <= 0)
            {
                originalX = transform.position.x;
                enemyBody.linearVelocity = Vector2.zero;
                isInvulnerable = false;
            }
            
        }
        else if(playerTransform != null)
        {
            // Calculate horizontal direction to the player (-1 for Left, 1 for Right)
            float directionToPlayer = Mathf.Sign(playerTransform.position.x - transform.position.x);

            enemyBody.linearVelocity = new Vector2(directionToPlayer * moveSpeed, enemyBody.linearVelocity.y);

            // Flip the Goomba to face the player
            if (directionToPlayer > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with goomba!");
            Time.timeScale = 0.0f;
            gameManager.MainGameScreen.SetActive(false);
            gameManager.GameOverScreen.SetActive(true);
        }
    }
}