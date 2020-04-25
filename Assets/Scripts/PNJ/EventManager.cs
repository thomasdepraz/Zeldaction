using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //Etats Des Combats (finis ou hors service)
    public bool combat01isEnded = false;
    public bool combat01isHS = false;

    //PNJ Axos et Crabes s'activant à l'issu du premier Combat.
    public GameObject[] axosInTheVillage;
    public GameObject[] crabesInTheVillage;

    //FirstFight Reference
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
