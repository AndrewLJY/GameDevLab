using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IPowerup;

public class BuffStateController : StateController
{
    public PowerupType currentPowerupType = PowerupType.Default;
    public BuffState shouldBeNextState = BuffState.Default;

    private SpriteRenderer spriteRenderer;

    public GameConstants gameConstants;

    public AudioSource musicSource; // drag the child object's AudioSource here

    public override void Start()
    {
        base.Start();
        GameRestart(); // clear powerup in the beginning, go to start state
    }

    // this should be added to the GameRestart EventListener as callback
    public void GameRestart()
    {
        // clear powerup
        currentPowerupType = PowerupType.Default;
        // set the start state
        TransitionToState(startState);
    }

    public void SetPowerup(PowerupType i)
    {
        currentPowerupType = i;
    }

    public void SetRendererToFlicker()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(BlinkSpriteRenderer());
    }

    private IEnumerator BlinkSpriteRenderer()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        while (string.Equals(currentState.name, "StarmanMario", StringComparison.OrdinalIgnoreCase))
        {
            spriteRenderer.color = spriteRenderer.color == Color.white ? Color.black : Color.white;

            // Wait for the specified blink interval
            yield return new WaitForSeconds(gameConstants.starmanInterval);
        }
        
        spriteRenderer.color = Color.white;

    }

    public void PlayStarmanMusic(AudioClip clip)
    {
        if (musicSource.isPlaying) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopStarmanMusic()
    {
        musicSource.Stop();
    }


}