using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public GameObject hook;
    public GameObject aimDirectionPreview;

    [SerializeField] public static bool hasHook;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasHook)
        {
            hook.SetActive(false);
            aimDirectionPreview.SetActive(false);
        }
        else       
        {
            hook.SetActive(true);
            aimDirectionPreview.SetActive(true);
        }
    }
}
