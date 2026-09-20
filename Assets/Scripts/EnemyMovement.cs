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
    public Vector3 startPosition; 

    private float knockbackTimer = 0f;
    private Transform playerTransform;

    void Awake()
    {
        startPosition = transform.localPosition;
        Debug.Log("startPosition: " + startPosition);
    }

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

    public void TakeDamage(Collider2D other, float knockbackForce)
    {
        if (!isInvulnerable)
        {
            animator.SetTrigger("onHit");
            Vector2 heading = transform.position - other.transform.position;
            Vector2 knockbackDirection = new Vector2(Mathf.Sign(heading.x), 0.3f).normalized;

            ApplyKnockback(knockbackDirection, knockbackForce);

            enemyHealth -= 1;
            Debug.Log("enemy.enemyHealth" + enemyHealth);

            if (enemyHealth < 1)
            {
                Destroy(gameObject);
                GameManager.Instance.AddScore(1);
            }
        }
        else
        {
            Debug.Log("ITS INVULNERABLEEEE");
        }
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
            other.GetComponent<PlayerMovement>().TakeDamage(gameObject.GetComponent<Collider2D>());
        }
    }
}