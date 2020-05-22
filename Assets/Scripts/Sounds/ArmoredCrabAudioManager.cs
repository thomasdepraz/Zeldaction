using UnityEngine;
using UnityEngine.Audio;

public class ArmoredCrabAudioManager : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioMixerGroup output;
    public AudioClip shoot;
    public AudioClip armorDown;

    public void PlayClip(AudioClip clip, float volume, AudioMixerGroup mixer)
    {
        float pitch = Random.Range(0.8f, 1.2f);
        soundSource.pitch = pitch;
        soundSource.outputAudioMixerGroup = mixer;
        soundSource.volume = volume;
        soundSource.clip = clip;
        soundSource.Play();
    }
}
