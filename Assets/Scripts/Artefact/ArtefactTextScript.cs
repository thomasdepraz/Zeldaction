using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtefactTextScript : MonoBehaviour
{
    Text text;
    public static int artefactCounter;
    
    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
       text.text = artefactCounter.ToString(); 
    }
}
