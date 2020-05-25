using UnityEngine;
using UnityEngine.Audio;

public class BossAudioSource : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioSource audioSource02;

    //Audio Mixer Groups
    public AudioMixerGroup attack;
    public AudioMixerGroup cutscenes;
    public AudioMixerGroup health;

    //Sounds

    public AudioClip BalayagePince;
    public AudioClip FrappePatte;
    public AudioClip ImpactRoche;
    public AudioClip ImpactSaut;
    public AudioClip Invocation;
    public AudioClip MortBoss;
    public AudioClip PatteCassée;
    public AudioClip PriseDégats;
    public AudioClip Saut;

    public void PlayClip(AudioSource source, AudioClip clip, float volume, AudioMixerGroup mixer)
    {
        float pitch = Random.Range(0.8f, 1.2f);
        source.outputAudioMixerGroup = mixer;
        source.pitch = pitch;
        source.volume = volume;
        source.clip = clip;
        source.Play();
    }

    public void PlayClipNat(AudioSource source, AudioClip clip, float volume, AudioMixerGroup mixer)
    {
        source.outputAudioMixerGroup = mixer;
        source.pitch = 1;
        source.volume = volume;
        source.clip = clip;
        source.Play();
    }
}