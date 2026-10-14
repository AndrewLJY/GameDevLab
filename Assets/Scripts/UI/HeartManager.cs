using UnityEngine;
using TMPro;

public class HeartManager : MonoBehaviour
{
    public Animator heartAnimator;

    void Start()
    {
        heartAnimator = GetComponent<Animator>();
    }

    public void PlayerTakingDmg()
    {
        heartAnimator.SetTrigger("TakeDamage");
    }

    public void DeactivateHeart()
    {
        gameObject.SetActive(false);
    }
}
