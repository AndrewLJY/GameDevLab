using UnityEngine;
using UnityEngine.Audio;

public class WeaponAttack : MonoBehaviour
{
    private Animator animator;

    public AudioSource weaponAudio;
    public AudioClip weaponSwing;

    private float knockbackForce = 2f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!weaponAudio.isPlaying)
            {
                weaponAudio.PlayOneShot(weaponSwing);
            }
            other.GetComponent<EnemyController>().TakeDamage(gameObject.GetComponent<Collider2D>(), knockbackForce);

        }

    }

    public void PlaySwingAnim()
    {
        animator.SetTrigger("Swing");
    }
}
