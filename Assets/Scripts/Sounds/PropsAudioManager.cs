using UnityEngine;
using UnityEngine.Audio;

public class PropsAudioManager : MonoBehaviour
{
    public AudioSource soundSource;

    public AudioMixerGroup rocks;
    public AudioMixerGroup plates;
    public AudioMixerGroup fires;
    public AudioMixerGroup crate;

    public AudioClip hitRock;
    public AudioClip breakRock;
    public AudioClip onPressurePlate;
    public AudioClip openDoor;
    public AudioClip lightFire;
    public AudioClip heavyCrateMove;
    public AudioClip lightCrateMove;
    public AudioClip lightCratebreak;
    public AudioClip fireCamp;


    public void PlayClip(AudioClip clip, float volume, AudioMixerGroup mixer)
    {
        float pitch = Random.Range(0.8f, 1.2f);
        soundSource.pitch = pitch;
        soundSource.volume = volume;
        soundSource.loop = false;
        soundSource.outputAudioMixerGroup = mixer;
        soundSource.clip = clip;
        soundSource.Play();
    }

    public void PlayClipNat(AudioClip clip, float volume, AudioMixerGroup mixer)
    {
        soundSource.loop = false;
        soundSource.volume = volume;
        soundSource.outputAudioMixerGroup = mixer;
        soundSource.clip = clip;
        soundSource.Play();
    }

    public void PlayAndLoop(AudioClip clip, float volume, AudioMixerGroup mixer)
    {
        soundSource.volume = volume;
        soundSource.outputAudioMixerGroup = mixer;
        soundSource.clip = clip;
        soundSource.loop = true;
        soundSource.Play();
    }

}
