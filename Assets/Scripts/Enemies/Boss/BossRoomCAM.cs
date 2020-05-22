using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomCAM : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject phase1Cam;
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
    }
}
