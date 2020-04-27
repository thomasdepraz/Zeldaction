using UnityEngine;
using Cinemachine;

public class CameraTransition : MonoBehaviour
{
    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera transitionCam;


    // Transition de caméra 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerCam.gameObject.SetActive(false);
        transitionCam.gameObject.SetActive(true);
    }
}
