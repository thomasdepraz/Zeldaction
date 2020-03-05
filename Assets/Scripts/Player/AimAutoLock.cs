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
    private SpriteRenderer rend;
    public GameObject lockGuizmo;


    //Logic
    private GameObject[] hookables;
    private float distanceToHookable;
    [HideInInspector] public bool locked;
    private bool canLock = true;
    private RaycastHit2D hit;
    public LayerMask playerLayerMask;
    


    [Header("Tweak")]
    [Range(0f, 1f)]
    public float autoLockDistance = 0.1f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//Get the player
        playerAim = player.GetComponent<PlayerAim>();
        rend = gameObject.GetComponent<SpriteRenderer>();
        hookables = GameObject.FindGameObjectsWithTag("Hookable");//Get every hookable object in the scene
        
    }

    void Update()
    {
        playerLayerMask = ~playerLayerMask;
        CheckHookables();

        if (locked && playerAim.distanceToPlayer >= playerAim.maxRange)//If player is too far from locked crosshair, then it unlocks
        {
            StartCoroutine("CanLock");
            locked = false;     
        }

        if(!locked)
            gameObject.transform.SetParent(player.transform);//stick back to player
    }

    void CheckHookables()
    {       
        Vector2 direction = (Vector2)(gameObject.transform.position - player.transform.position).normalized;
       
       if(!locked)
       {
            hit = Physics2D.Raycast(player.transform.position, direction, playerAim.maxRange, playerLayerMask);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Hookable"))
                {
                    Debug.DrawRay(player.transform.position, direction, Color.red);
                    //Le hookable est repéré, il faut lock ici.

                    //Le viseur disparait
                    rend.enabled = false;
                    //le marqueur apparait et s'accroche à la cible
                    lockGuizmo.SetActive(true);
                    lockGuizmo.transform.position = hit.collider.gameObject.transform.position;
                    lockGuizmo.transform.SetParent(hit.collider.gameObject.transform);
                    locked = true;
                }
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