using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScale : MonoBehaviour
{
    public CinemachineVirtualCamera liveVirtualCam;
    public CinemachineBrain cameraBrain;
    public float lens;

    private void Start()
    {      
        cameraBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        liveVirtualCam = cameraBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
        if (collision.gameObject.CompareTag("Player"))
            StartCoroutine("blendCamera");
    }

    private IEnumerator blendCamera()
    {
        if (liveVirtualCam.m_Lens.OrthographicSize < lens)
        {
            while(liveVirtualCam.m_Lens.OrthographicSize < lens)
            {
                liveVirtualCam.m_Lens.OrthographicSize += 0.02f;
                yield return new WaitForSeconds(0.01f);
            }
        }

        else
        {
            while (liveVirtualCam.m_Lens.OrthographicSize > lens)
            {
                liveVirtualCam.m_Lens.OrthographicSize -= 0.02f;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
