
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using static IPowerup;

public class FireFlowerPowerup : BasePowerup
{
    // setup this object's type
    // instantiate

    private AudioSource powerupAudioSource;
    private Vector3 initialPowerupPosition;
    private Vector3 initialPowerupColliderPosition;
    private Vector3 powerUpAudioSourcePosition;

    public GameConstants gameConstants;
    public AudioClip powerupApplySFX;

    public int scoreValue = 10;

    GameManager gameManager;

    public UnityEvent<int> increaseScore;

    void Awake()
    {
    }

    protected override void Start()
    {
        initialPowerupColliderPosition = transform.localPosition;
        initialPowerupPosition = transform.GetChild(0).transform.localPosition;

        base.Start(); // call base class Start()
        this.type = PowerupType.FireFlower;
        powerupAudioSource = GetComponent<AudioSource>();
        powerUpAudioSourcePosition = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && spawned)
        {
            AudioSource.PlayClipAtPoint(powerupApplySFX, powerUpAudioSourcePosition, 1.0f);

            ApplyPowerup(col.gameObject.GetComponent<MonoBehaviour>());

            increaseScore.Invoke(scoreValue);

            gameObject.SetActive(false);

        }
        else if (col.gameObject.layer == 12)
        {
            if (spawned)
            {
                goRight = !goRight;
                //rigidBody.AddForce(Vector2.right * gameConstants.powerupSpeedMultiplier * (goRight ? 1 : -1), ForceMode2D.Impulse);

            }
        }
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        powerupAudioSource.PlayOneShot(powerupAudioSource.clip);

        powerupCollider.enabled = true;
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        spawned = true;

        //await Task.Delay(1);
        //rigidBody.AddForce(Vector2.right * gameConstants.powerupSpeedMultiplier, ForceMode2D.Impulse);
    }


    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        powerupAudioSource.PlayOneShot(powerupApplySFX);
        base.ApplyPowerup(i);
        // try
        MarioStateController mario;
        bool result = i.TryGetComponent<MarioStateController>(out mario);
        if (result)
        {
            mario.SetPowerup(this.powerupType);
        }

    }

    public void GameRestart()
    {
        if (powerupCollider != null && rigidBody != null)
        {
            gameObject.SetActive(false);
            powerupCollider.enabled = false;
            rigidBody.bodyType = RigidbodyType2D.Static;
            spawned = false;

            transform.localPosition = initialPowerupColliderPosition;
            transform.GetChild(0).transform.localPosition = initialPowerupPosition;
            gameObject.SetActive(true);
        }
    }
}