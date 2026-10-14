using UnityEngine;
using UnityEngine.Audio;

public class WeaponAttack : MonoBehaviour
{
    private Animator animator;

    public AudioSource weaponAudio;
    public AudioClip weaponSwing;

    public GameConstants gameConstants;

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
            other.GetComponent<EnemyController>().TakeDamage(gameObject.GetComponent<Collider2D>(), gameConstants.weaponKnockbackForce);

        }

    }

    public void PlaySwingAnim()
    {
        animator.SetTrigger("Swing");
    }
}
