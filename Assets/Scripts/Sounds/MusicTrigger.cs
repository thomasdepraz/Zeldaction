﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public MusicManager soundManager;
    public AudioClip nextAmbiantSound;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(nextAmbiantSound != soundManager.audioSource.clip)
            {
                soundManager.CrossfadeStart(nextAmbiantSound);
            }
        }
    }
}
