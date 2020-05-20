using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudioManager : MonoBehaviour
{

    public AudioSource soundSource;

    //Audio mixer Groups
    public AudioMixerGroup steps;
    public AudioMixerGroup life;
    public AudioMixerGroup hook;
    public AudioMixerGroup attack;
    public AudioMixerGroup misc;

    //Sounds
    public AudioClip step;

    public AudioClip onHit;
    public AudioClip lifeUp;
    public AudioClip death;

    public AudioClip lightAttack;
    public AudioClip heavyAttack;

    public AudioClip onHook;
    public AudioClip hookThrow;
    public AudioClip hookPull;

    public AudioClip heavyAttackLoad;


    public void PlayClip(AudioClip clip, float volume, AudioMixerGroup mixer)
    {
        float pitch = Random.Range(0.8f, 1.2f);
        soundSource.outputAudioMixerGroup = mixer;
        soundSource.pitch = pitch;
        soundSource.volume = volume;
        soundSource.clip = clip;
        soundSource.Play();
    }

    public void PlayClipNat(AudioClip clip, float volume, AudioMixerGroup mixer)
    {
        soundSource.outputAudioMixerGroup = mixer;
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
