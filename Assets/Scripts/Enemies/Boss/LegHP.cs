﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegHP : MonoBehaviour
{
    [Range(0, 10)]
    public int maxHealth = 3;
    public int currentHealth;
    SpriteRenderer sr;
    public Animator anim;
    public Sprite BrokenLegSprite;
    public BossAudioManager bossAudio;
    // Start is called before the first frame update
    private void Awake()
    {
        bossAudio = GameObject.Find("BossAudioSource").GetComponent<BossAudioManager>();
    }
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // le montant des dommages va être soustrait à la vie actuelle de l'ennemi
        bossAudio.PlayClip(bossAudio.soundSource, bossAudio.PriseDégats, 1, bossAudio.health);
        if (currentHealth > 0)
        {
            StartCoroutine(DamageFB());
        }
        if (currentHealth <= 0)
        {
            anim.SetTrigger("Broken");
            GetComponent<Hookable>().isActive = true;
            GetComponent<BossLegProjectile>().Broken = true;
        }
    }

    IEnumerator DamageFB()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }
}
