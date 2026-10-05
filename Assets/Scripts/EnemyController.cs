using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [System.NonSerialized] public Animator enemyAnimator;
    [System.NonSerialized] public bool isInvulnerable = false;
    [System.NonSerialized] public Vector3 startPosition;

    private float originalX;
    private float moveSpeed = 0.5f;
    private float knockbackTimer = 0f;
    private Rigidbody2D enemyBody;
    private Transform playerTransform;
    private bool isDead = false;

    public int enemyHealth = 3;

    GameManager gameManager;

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
        enemyAnimator = GetComponent<Animator>();

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
        if (!isInvulnerable && !isDead)
        {
            enemyAnimator.SetTrigger("onHit");
            enemyHealth -= 1;
            Debug.Log("Enemy hit! Current health: " + enemyHealth);

            Vector2 heading = transform.position - other.transform.position;
            Vector2 knockbackDirection = new Vector2(Mathf.Sign(heading.x), 0.3f).normalized;

            ApplyKnockback(knockbackDirection, knockbackForce);

            if (enemyHealth <= 0)
            {
                enemyAnimator.SetTrigger("onDead");
                isDead = true;

            }
        }
    }

    public void OnDead()
    {
        isInvulnerable = false;
        gameObject.SetActive(false);
        gameManager.IncreaseScore(1);
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
        //enemyAnimator.SetTrigger("gameRestart");

        transform.localPosition = startPosition;
        originalX = transform.position.x;

        enemyHealth = 3;
        isDead = false;
        isInvulnerable = false;
        knockbackTimer = 0f;

        enemyAnimator.ResetTrigger("onHit");
        enemyAnimator.ResetTrigger("onDead");

        enemyAnimator.Play("Idle");
    }
}