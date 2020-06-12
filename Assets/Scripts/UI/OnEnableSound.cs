using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableSound : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<AudioSource>().Play();
    }
}
