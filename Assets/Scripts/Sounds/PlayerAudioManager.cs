using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudioManager : MonoBehaviour
{

    public AudioSource soundSource;
    public AudioClip lightAttack;
    public AudioClip heavyAttack;
    public AudioClip lifeUp;
    public AudioClip onHook;


    public void PlayClip(AudioClip clip, float volume)
    {
        float pitch = Random.Range(0.8f, 1.2f);
        soundSource.pitch = pitch;
        soundSource.volume = volume;
        soundSource.clip = clip;
        soundSource.Play();
    }

    public void PlayClipDelay(AudioClip clip, float delay)
    {
        float pitch = Random.Range(0.8f, 1.2f);
        soundSource.pitch = pitch;
        soundSource.clip = clip;
        soundSource.PlayDelayed(delay);
    }
}
