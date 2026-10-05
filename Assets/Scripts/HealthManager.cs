using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public static GameManager Instance;
    public Animator healthAnimator;

    void Start()
    {
        healthAnimator = GetComponent<Animator>();
    }

    public void PlayerTakingDmg()
    {
        healthAnimator.SetTrigger("TakeDamage");
    }

    public void DeactivateHeart()
    {
        gameObject.SetActive(false);
    }
}
