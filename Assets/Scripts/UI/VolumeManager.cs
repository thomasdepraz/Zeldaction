﻿using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    //Master Mixers
    public AudioMixer player;
    public AudioMixer props;
    public AudioMixer UI;
    public AudioMixer enemy;
    public AudioMixer boss;
    public AudioMixer music;
    public AudioMixer environment;

    public void setVolume(float volume)
    {
        player.SetFloat("MyExposedParam", volume);
        props.SetFloat("MyExposedParam", volume);
        UI.SetFloat("MyExposedParam", volume);
        enemy.SetFloat("MyExposedParam", volume);
        boss.SetFloat("MyExposedParam", volume);
        music.SetFloat("MyExposedParam", volume);
        environment.SetFloat("MyExposedParam", volume);
    }
}
