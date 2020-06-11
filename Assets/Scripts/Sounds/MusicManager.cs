using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip mainTheme;
    public AudioClip chillTheme1;
    public AudioClip chillTheme2;
    public AudioClip bossFight;
    public AudioClip sadEpicTheme;
    public AudioClip clapPercu;
    public AudioClip tensionPercu;
    public AudioClip rythmicTamtam;

    private bool canStartCoroutine = true;

    private void Start()
    {
        audioSource.Play();
    }
    public void CrossfadeStart(AudioClip clip)
    {
        if (canStartCoroutine)
        {
            StartCoroutine(Crossfade(clip));
        }
    }
    private IEnumerator Crossfade(AudioClip nextClip)
    {
        canStartCoroutine = false;
        while (audioSource.volume > 0)
        {
            yield return new WaitForSeconds(0.1f);
            audioSource.volume -= 0.1f;
        }

        audioSource.clip = nextClip;
        audioSource.Play();

        while (audioSource.volume < 1)
        {
            yield return new WaitForSeconds(0.1f);
            audioSource.volume += 0.1f;
        }
        canStartCoroutine = true;
    }
}
