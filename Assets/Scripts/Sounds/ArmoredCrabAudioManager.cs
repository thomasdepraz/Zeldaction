using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredCrabAudioManager : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioClip lightAttack;

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
