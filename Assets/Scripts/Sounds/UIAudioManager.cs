
using UnityEngine;
using UnityEngine.Audio;

public class UIAudioManager : MonoBehaviour
{

    public AudioSource soundSource;

    public AudioMixerGroup menu;
    public AudioMixerGroup dialog;
    public AudioMixerGroup artefactOutput;

    public AudioClip openMenu;
    public AudioClip closeMenu;
    public AudioClip navigation;
    public AudioClip validation;
    public AudioClip openDialog;
    public AudioClip closeDialog;
    public AudioClip artefact;
    public AudioClip forcedDialog;

    public void PlayClip(AudioSource source, AudioClip clip, float volume, AudioMixerGroup mixer)
    {
        source.outputAudioMixerGroup = mixer;
        source.pitch = 1;
        source.volume = volume;
        source.clip = clip;
        source.Play();
    }
}
