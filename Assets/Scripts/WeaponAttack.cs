using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    private Animator animator;

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
        if (other.gameObject.CompareTag("Enemy"))
        {

            Debug.Log("We sliced the enemy!");
            other.GetComponent<EnemyMovement>().TakeDamage(gameObject.GetComponent<Collider2D>(), knockbackForce);

        }

    }
}
