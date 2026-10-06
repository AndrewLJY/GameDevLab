using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixerSnapshot defaultSnapshot;
    public AudioMixerSnapshot mutedSnapshot;

    public void GameRestart()
    {
        mutedSnapshot.TransitionTo(0f);
    }

    public void RestoreAudio()
    {
        defaultSnapshot.TransitionTo(0.5f);
    }
}