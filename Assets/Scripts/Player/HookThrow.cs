﻿using UnityEngine;
using System.Collections;
using Player;
using UnityEngine.UI;

public class HookThrow : MonoBehaviour
{

    [Header("Elements")]
    public GameObject hook;
    private Rigidbody2D hookRigidBody;
    public GameObject crosshair;
    private PlayerMovement playerMovement;
    private PlayerAim playerAim;
    public Sprite hookAvailable;
    public Sprite hookUnavailable;
    public Image hookUI;

    [Header("Logic")]
    [Range(0f, 10f)]
    public float speed;
    private Vector3 direction;
    private bool isThrown = false;
    [HideInInspector]public bool isHooked = false;
    [HideInInspector]public bool isPulling = false;
    private bool canStartCoroutine = true;


    [Header("Tweak")]
    [Range(0f, 5f)]
    public float hookDetectionRange;
    [Range(0f, 5f)]
    public float timeToCancel = 3f;


    // Start is called before the first frame update
    void Start()
    {
        hookRigidBody = hook.GetComponent<Rigidbody2D>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerAim = gameObject.GetComponent<PlayerAim>();
    }

    // Update is called once per frame
    void Update()
    {

        if(PlayerManager.hasHook)
        {
            if(Input.GetButtonDown("Throw") && !isThrown && !isHooked && playerAim.isAiming)//Si le hameçon n'est pas lancé et qu'on appui sur R1 alors on le lance.
            {
                hookRigidBody.simulated = true;
                Throw();
            }
            else if (Input.GetButtonDown("Throw") && isThrown && !isPulling)// si l'hameçon est lancée et qu'on appuie sur R1 on le tire
            {
                //Le player peut plus bouger
                playerMovement.canMove = false;
                playerMovement.playerRb.velocity = Vector2.zero;
                Pull();
                isPulling = true;
            
            }
            if (isThrown) //Quand le hameçon est lancé on vérifie s'il peut se hook
            {
                Hook();
            }
        }
    }

    void Throw()
    {
        direction = (crosshair.transform.position - hook.transform.position).normalized; //direction player-> crosshair
        hookRigidBody.velocity += (Vector2)direction * speed; //déplacement du hook
        isThrown = true;
        hookUI.sprite = hookUnavailable;
    }

    public void Pull()
    {
        if (isHooked && hook.transform.parent.GetComponent<Hookable>().isActive)
        {
            if(hook.transform.parent.GetComponent<Hookable>().isLight)//si le truc est léger
            {
                hookRigidBody.simulated = false;
                hook.transform.parent.GetComponent<Rigidbody2D>().drag = 0f;
                direction = (transform.position - hook.transform.position);
                hook.transform.parent.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed * 2;
            }
            else if(hook.transform.parent.GetComponent<Hookable>().isHeavy)//si le truc est lourd
            {
                Physics2D.IgnoreLayerCollision(10, 14, true);
                direction = (hook.transform.position - transform.position);
                gameObject.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed * 2;//on se déplace vers l'objet
            }
            else if (hook.transform.parent.GetComponent<Hookable>().isUndergroundCrab)//si le truc est un crab soutterain
            {
                hook.transform.parent.GetComponent<CrabeSouterrain>().isPulled();
                hook.transform.SetParent(gameObject.transform);
                hookRigidBody.simulated = true;

                direction = (transform.position - hook.transform.position);
                hookRigidBody.velocity = Vector2.zero;
                hookRigidBody.velocity = direction.normalized * speed * 2;
            }
            else if(hook.transform.parent.GetComponent<Hookable>().isArmoredCrab)//si c'est un crabe lourd
            {
                GameObject ArmoredCrab = hook.transform.parent.gameObject;
                ArmoredCrab.GetComponent<ArmoredCrab>().anim.SetBool("isDead", true);
                GameObject Armor = Instantiate(ArmoredCrab.GetComponent<ArmoredCrab>().armor, ArmoredCrab.transform.position, Quaternion.identity);
                hook.transform.SetParent(Armor.transform);

                Pull();
            }
        }
        else //Retour de l'hameçon
        {
            direction = (transform.position - hook.transform.position);
            hookRigidBody.velocity = Vector2.zero;
            hookRigidBody.velocity = direction.normalized * speed * 2;
        }
    }

    void Hook()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(hook.transform.position, hookDetectionRange);
        foreach (Collider2D hookable in hit)
        {
            if(hookable.gameObject.CompareTag("Hookable") && !isHooked && isThrown)
            {
                if(hookable.gameObject.GetComponent<Hookable>().isActive)
                {
                    //S'accrocher à l'objet
                    hook.transform.position = hookable.gameObject.transform.position;
                    hook.transform.SetParent(hookable.gameObject.transform);
                    hookRigidBody.velocity = Vector2.zero;
                    hookRigidBody.simulated = false;
                    isHooked = true;

                    if (isPulling)
                        Pull();

                    if (canStartCoroutine)
                        StartCoroutine("HookCancel");
                }  
            }

            if(hookable.gameObject.CompareTag("Player") && isPulling)
            {
                if (isHooked)
                {
                    hook.transform.parent.GetComponent<Rigidbody2D>().drag = 3f;
                    hook.transform.SetParent(gameObject.transform);
                    isHooked = false;
                    Physics2D.IgnoreLayerCollision(10, 14, false);
                }

                hookRigidBody.velocity = Vector2.zero;
                hookRigidBody.simulated = false;
                isThrown = false;
                isPulling = false;
                playerMovement.canMove = true;
                hookUI.sprite = hookAvailable;
            }
        }
    }

    private IEnumerator HookCancel()
    {
        canStartCoroutine = false;
        yield return new WaitForSeconds(timeToCancel);
        if(!isPulling && !hook.transform.parent.CompareTag("Player"))
        {
            isPulling = true;
            playerMovement.canMove = false;
            playerMovement.playerRb.velocity = Vector2.zero;
            hook.transform.SetParent(gameObject.transform);
            hookRigidBody.simulated = true;

            direction = (transform.position - hook.transform.position);
            hookRigidBody.velocity = Vector2.zero;
            hookRigidBody.velocity = direction.normalized * speed * 2;
        }
        canStartCoroutine = true;
    }
}
