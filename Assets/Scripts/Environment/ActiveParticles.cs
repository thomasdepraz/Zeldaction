using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveParticles : MonoBehaviour
{

    public GameObject particlesToActivate;
    public PressurePlate linkedPressureplate;
    private AudioSource audioSource;
    public AudioClip clip;

    private void Start()
    {
        audioSource = particlesToActivate.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if(!particlesToActivate.activeSelf)
        {
            if(linkedPressureplate.isPressed)
            {
                particlesToActivate.SetActive(true);
                if(!audioSource.isPlaying)
                {
                    if(audioSource.clip != clip)
                    {
                        audioSource.clip = clip;
                        audioSource.Play();
                    }
                }
            }
        }
    }
}
