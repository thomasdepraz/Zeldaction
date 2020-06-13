using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicTrigger : MonoBehaviour
{
    public MusicManager soundManager;
    public AudioClip nextAmbiantSound;
    public AudioMixerGroup mixer; 
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(nextAmbiantSound != soundManager.audioSource.clip)
            {
                soundManager.audioSource.outputAudioMixerGroup = mixer;
                soundManager.CrossfadeStart(nextAmbiantSound);
            }
        }
    }
}
