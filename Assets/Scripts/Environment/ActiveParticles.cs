using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveParticles : MonoBehaviour
{

    public GameObject particlesToActivate;
    public PressurePlate linkedPressureplate;

    // Update is called once per frame
    void Update()
    {
        if(!particlesToActivate.activeSelf)
        {
            if(linkedPressureplate.isPressed)
            {
                particlesToActivate.SetActive(true);
            }
        }
    }
}
