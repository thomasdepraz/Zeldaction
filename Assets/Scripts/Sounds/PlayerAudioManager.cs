using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudioManager : MonoBehaviour
{

    public AudioSource soundSource;
    public AudioSource stepSoundSource;
    public AudioSource hookSoundSource;

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
        source.pitch =1;
        source.volume = volume;
        source.clip = clip;
        source.Play();
    }
}
