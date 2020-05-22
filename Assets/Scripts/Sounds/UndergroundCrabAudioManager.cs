using UnityEngine;
using UnityEngine.Audio;


public class UndergroundCrabAudioManager : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioMixerGroup output;
    public AudioClip move;
    public AudioClip death;
    public AudioClip clawOut;
    public AudioClip clawIn;

    public void PlayClip(AudioClip clip, float volume, AudioMixerGroup mixer)
    {
        float pitch = Random.Range(0.8f, 1.2f);
        soundSource.loop = false;
        soundSource.pitch = pitch;
        soundSource.outputAudioMixerGroup = mixer;
        soundSource.volume = volume;
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
