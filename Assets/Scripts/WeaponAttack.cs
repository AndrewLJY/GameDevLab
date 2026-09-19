using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    private Animator animator;
    private Vector2 knockback;

    private float knockbackForce = 2f;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Change to your attack button/key
        {
            Debug.Log("Swingggggggggggggggg");
            animator.SetTrigger("Swing");

        }
    }

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) {
            Debug.Log("We sliced the enemy!");
            EnemyMovement enemy = other.GetComponent<EnemyMovement>();

            Vector2 heading = other.transform.position - transform.position;
            Vector2 knockbackDirection = new Vector2(Mathf.Sign(heading.x), 0.3f).normalized;

            enemy.ApplyKnockback(knockbackDirection, knockbackForce);

            enemy.enemyHealth -= 1;
            Debug.Log("enemy.enemyHealth" + enemy.enemyHealth);

            if(enemy.enemyHealth < 1)
            {
                Destroy(other.gameObject);
            }
        }
            
    }
}
