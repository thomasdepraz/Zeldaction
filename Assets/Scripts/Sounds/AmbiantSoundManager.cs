using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AmbiantSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip beachAmbiance;
    public AudioClip jungleAmbiance;
    public AudioClip jungle2Ambiance;
    public AudioClip stormAmbiance;
    public AudioClip dungeonAmbiance;

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
            Debug.Log("Allo2");
            yield return new WaitForSeconds(0.1f);
            audioSource.volume += 0.1f;
        }
        canStartCoroutine = true;
    }
}
