using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class AimAutoLock : MonoBehaviour
{
    //Elements 
    [Header("Elements")]
    public GameObject player;
    private PlayerAim playerAim;


    //Logic
    private GameObject[] hookables;
    private float distanceToHookable;
    [HideInInspector] public bool locked;
    private bool canLock = true;

    [Header("Tweak")]
    [Range(0f, 1f)]
    public float autoLockDistance = 0.1f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//Get the player
        playerAim = player.GetComponent<PlayerAim>();

        hookables = GameObject.FindGameObjectsWithTag("Hookable");//Get every hookable object in the scene
        
    }

    void Update()
    {
        CheckHookables();

        if(locked && playerAim.distanceToPlayer >= playerAim.maxRange)//If player is too far from locked crosshair, then it unlocks
        {
            StartCoroutine("CanLock");
            locked = false;     
        }

        if(!locked)
            gameObject.transform.SetParent(player.transform);//stick back to player
    }

    void CheckHookables()
    {
        for(int i = 0; i < hookables.Length; i++)
        {
            distanceToHookable = (gameObject.transform.position - hookables[i].transform.position).magnitude;

            if(distanceToHookable <= autoLockDistance && !locked  && canLock)
            {
                Debug.Log(hookables.ToString());
                canLock = false;
                locked = true;
                gameObject.transform.position = hookables[i].transform.position; //crosshair pos to target pos
                gameObject.transform.SetParent(hookables[i].transform); //crossair sticks to target
            }
        }
    }

    public IEnumerator CanLock()
    {
        yield return new WaitForSeconds(0.5f);
        canLock = true;
        yield return null;
    }
}