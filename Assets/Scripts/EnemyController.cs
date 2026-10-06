using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [System.NonSerialized] public Animator animator;
    [System.NonSerialized] public bool isInvulnerable = false;
    [System.NonSerialized] public Vector3 startPosition;

    private float originalX;
    private float moveSpeed = 0.5f;
    private float knockbackTimer = 0f;
    private Rigidbody2D enemyBody;
    private Transform playerTransform;

    public int enemyHealth = 3;

    GameManager gameManager;

    void Awake()
    {
        startPosition = transform.localPosition;
    }

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();

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

    public void TakeDamage(Collider2D other, float knockbackForce)
    {
        if (!isInvulnerable)
        {
            animator.SetTrigger("onHit");
            Vector2 heading = transform.position - other.transform.position;
            Vector2 knockbackDirection = new Vector2(Mathf.Sign(heading.x), 0.3f).normalized;

            ApplyKnockback(knockbackDirection, knockbackForce);

            enemyHealth -= 1;

            if (enemyHealth < 1)
            {
                animator.SetTrigger("onDead");
                gameManager.IncreaseScore(1);
            }
        }
    }

    public void OnDead()
    {
        gameObject.SetActive(false);
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
            float directionToPlayer = Mathf.Sign(playerTransform.position.x - transform.position.x);

            enemyBody.linearVelocity = new Vector2(directionToPlayer * moveSpeed, enemyBody.linearVelocity.y);

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
            other.GetComponent<PlayerController>().TakeDamage(gameObject.GetComponent<Collider2D>());
        }
    }

    public void GameRestart()
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        animator.SetTrigger("gameRestart");

        transform.localPosition = startPosition;
        originalX = transform.position.x;

        enemyHealth = 3;
        isInvulnerable = false;
        knockbackTimer = 0f;
    }
}