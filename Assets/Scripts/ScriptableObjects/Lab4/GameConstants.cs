using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    [Header("Default Values")]
    [SerializeField] private float defaultSpeed = 0.5f;
    [SerializeField] private float defaultMaxSpeed = 1f;
    [SerializeField] private float defaultUpSpeed = 0.1f;
    [SerializeField] private Vector3 defaultMarioPosition = new Vector3(-0.947f, -0.318f, 0);
    [SerializeField] private int defaultHealth = 3;

    [SerializeField] private int defaultWeaponDmg = 1;
    [SerializeField] private int defaultWeaponKnockbackForce = 2;

    [SerializeField] private float defaultEnemyMoveSpeed = 0.5f;
    [SerializeField] private float defaultEnemyKnockbackForce = 0.1f;

    [SerializeField] private int defaultPowerupScoreValue = 10;
    [SerializeField] private float defaultPowerupSpeedMultiplier = 0.01f;

    [Header("Run-time Values")]
    [Header("Mario")]
    // Mario's movement
    public float speed = 0.5f;
    public float maxSpeed = 1f;
    public float upSpeed = 0.1f;
    public Vector3 marioStartingPosition = new Vector3(-0.947f, -0.318f, 0);
    public int health = 3;

    [Header("Weapon")]
    // Weapon
    public int weaponDmg = 1;
    public int weaponKnockbackForce = 2;

    [Header("Enemy")]
    // Enemy
    public float enemyMoveSpeed = 0.5f;
    public float enemyKnockbackForce = 0.1f;

    [Header("Powerups")]
    // Powerups
    public int powerupScoreValue = 10;
    public float powerupSpeedMultiplier = 0.01f;

    [Header("Music")]
    // Music
    public AudioClip bgMusic;
    public AudioClip deathMusic;

    public float flickerInterval = 1f;

    public void ResetToDefaults()
    {
        speed = defaultSpeed;
        maxSpeed = defaultMaxSpeed;
        upSpeed = defaultUpSpeed;
        marioStartingPosition = defaultMarioPosition;
        health = defaultHealth;

        weaponDmg = defaultWeaponDmg;
        weaponKnockbackForce = defaultWeaponKnockbackForce;

        enemyMoveSpeed = defaultEnemyMoveSpeed;
        enemyKnockbackForce = defaultEnemyKnockbackForce;

        powerupScoreValue = defaultPowerupScoreValue;
        powerupSpeedMultiplier = defaultPowerupSpeedMultiplier;
    }
}