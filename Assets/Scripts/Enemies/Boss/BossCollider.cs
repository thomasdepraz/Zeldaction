using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollider : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject phase1Cam;
    public GameObject fxCollider;
    public GameObject Boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerCam.SetActive(false);
        phase1Cam.SetActive(true);
        gameObject.SetActive(false);
        fxCollider.SetActive(true);
        Boss.SetActive(true);
    }
}
