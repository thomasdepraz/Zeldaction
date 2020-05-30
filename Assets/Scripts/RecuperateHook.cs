using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RecuperateHook : MonoBehaviour
{
    bool inRange = false;
    public GameObject hook;
    public GameObject button;
    private SpriteRenderer hookSpr;
    private SpriteRenderer buttonSpr;
    public GameObject title;
    public GameObject buttonUI;
    public Animator anim;
    public AudioSource audioSource;
    bool coroutineLaunch = false;
    bool scalingIsFinish = false;
    public GameObject colliderArena;

    //Cam Transition
    public CinemachineVirtualCamera littleCam;
    public CinemachineVirtualCamera bigCam;
    public CinemachineVirtualCamera playerCam;
    

    void Start()
    {
        hookSpr = hook.GetComponent<SpriteRenderer>();
        buttonSpr = button.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        //Start Event
        if (inRange == true && Input.GetButtonDown("interact"))
        {
            StartEvent();           
        }

        //End Event
        if (scalingIsFinish == true && Input.GetButtonDown("interact"))
        {
            anim.SetBool("FadeOut", true);
            audioSource.Stop();
            bigCam.gameObject.SetActive(false);
            playerCam.gameObject.SetActive(true);
            colliderArena.SetActive(false);
            buttonUI.SetActive(false);
        }
    }  

    void StartEvent()
    {
        if(coroutineLaunch == false)
        {
            PlayerManager.hasHook = true;
            littleCam.gameObject.SetActive(false);
            bigCam.gameObject.SetActive(true);
            hookSpr.enabled = false;
            buttonSpr.enabled = false;
            title.SetActive(true);
            audioSource.Play();
            StartCoroutine(LoadScalingCam());
            coroutineLaunch = true;
        }        
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRange = true;
        button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
        button.SetActive(false);
    }

    IEnumerator LoadScalingCam()
    {
        yield return new WaitForSeconds(10f);
        scalingIsFinish = true;
        buttonUI.SetActive(true);
    }
}
