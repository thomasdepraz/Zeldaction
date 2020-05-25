using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    //Master Mixers
    public AudioMixer player;
    public AudioMixer props;
    public AudioMixer UI;
    public AudioMixer enemy;
    public AudioMixer boss;

    public void setVolume(float volume)
    {
        //volume *= 40;
        player.SetFloat("MyExposedParam", volume);
        props.SetFloat("MyExposedParam", volume);
        UI.SetFloat("MyExposedParam", volume);
        enemy.SetFloat("MyExposedParam", volume);
        boss.SetFloat("MyExposedParam", volume);
    }
}
