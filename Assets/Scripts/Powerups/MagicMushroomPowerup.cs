
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static IPowerup;

public class MagicMushroomPowerup : BasePowerup
{
    // setup this object's type
    // instantiate

    private AudioSource powerupAudioSource;
    private Vector3 initialPowerupPosition;
    private Vector3 initialPowerupColliderPosition;

    public GameConstants gameConstants;

    GameManager gameManager;

    void Awake()
    {
        GameManager.instance.gameRestart.AddListener(GameRestart);
    }

    protected override void Start()
    {
        gameManager = GameManager.instance;
        initialPowerupColliderPosition = transform.localPosition;
        initialPowerupPosition = transform.GetChild(0).transform.localPosition;

        base.Start(); // call base class Start()
        this.type = PowerupType.MagicMushroom;
        powerupAudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && spawned)
        {

            gameManager.IncreaseScore(gameConstants.powerupScoreValue);

            gameObject.SetActive(false);

        }
        else if (col.gameObject.layer == 12)
        {
            if (spawned)
            {
                goRight = !goRight;
                rigidBody.AddForce(Vector2.right * gameConstants.powerupSpeedMultiplier * (goRight ? 1 : -1), ForceMode2D.Impulse);

            }
        }
    }

    // interface implementation
    public async override void SpawnPowerup()
    {

        powerupAudioSource.PlayOneShot(powerupAudioSource.clip);

        powerupCollider.enabled = true;
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        spawned = true;

        await Task.Delay(1);
        rigidBody.AddForce(Vector2.right * gameConstants.powerupSpeedMultiplier, ForceMode2D.Impulse);
    }


    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        // TODO: do something with the object

    }

    public void GameRestart()
    {
        if (powerupCollider != null && rigidBody != null)
        {   
            powerupCollider.enabled = false;
            rigidBody.bodyType = RigidbodyType2D.Static;
            spawned = false;

            transform.localPosition = initialPowerupColliderPosition;
            transform.GetChild(0).transform.localPosition = initialPowerupPosition;
            gameObject.SetActive(true);
        }
    }
}