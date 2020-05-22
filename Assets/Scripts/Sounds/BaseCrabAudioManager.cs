using UnityEngine;
using UnityEngine.Audio;

public class BaseCrabAudioManager : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioMixerGroup output;
    public AudioClip chargeAttack;
    public AudioClip onHit;
    public AudioClip death;
    public void PlayClip(AudioClip clip, float volume, AudioMixerGroup mixer )
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
        soundSource.pitch = 1;
        soundSource.volume = volume;
        soundSource.clip = clip;
        soundSource.Play();
    }
}
