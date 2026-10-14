using UnityEngine;
using static IPowerup;


public abstract class BasePowerup : MonoBehaviour, IPowerup
{
    public PowerupType type;
    public bool spawned = false;
    protected bool consumed = false;
    protected bool goRight = true;
    protected Rigidbody2D rigidBody;
    protected Collider2D powerupCollider;

    // base methods
    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        powerupCollider = GetComponent<Collider2D>();
    }

    // interface methods
    // 1. concrete methods
    public PowerupType powerupType => type;

    public bool hasSpawned => spawned;

    public void DestroyPowerup()
    {
        Destroy(this.gameObject);
    }

    // 2. abstract methods, must be implemented by derived classes
    public virtual void SpawnPowerup() { }
    public virtual void ApplyPowerup(MonoBehaviour i) { }
        
}