using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class HookThrow : MonoBehaviour
{

    [Header("Elements")]
    public GameObject hook;
    private Rigidbody2D hookRigidBody;
    public GameObject crosshair;
    private PlayerAim playerAim;
    private PlayerMovement playerMovement;

    [Header("Logic")]
    [Range(0f, 10f)]
    public float speed;
    private Vector3 direction;
    private bool isThrown = false;
    private bool isHooked = false;
    private bool isPulled = false;
    private bool isPulling = false;
    private bool isHeavy;

    private float distancePlayerHook;

    [Header("Tweak")]
    [Range(0f, 5f)]
    public float hookDetectionRange;


    // Start is called before the first frame update
    void Start()
    {
        hookRigidBody = hook.GetComponent<Rigidbody2D>();
        playerAim = gameObject.GetComponent<PlayerAim>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Throw") && !isThrown)//Si le hameçon n'est pas lancé et qu'on appui sur R1 alors on le lance.
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
        if (isThrown && !isHooked) //Quand le hameçon est lancé on vérifie s'il peut se hook
        {
            Hook();
        }

     

        if (isPulled && distancePlayerHook <= 1)
        {
            if (isHooked && !isHeavy)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            else
            {
                hookRigidBody.velocity = Vector2.zero;
            }

            isPulled = false;
            isHooked = false;
            isThrown = false;
        }

    }

    void Throw()
    {
        direction = (crosshair.transform.position - hook.transform.position).normalized;
        hookRigidBody.velocity += (Vector2)direction * speed;
        isThrown = true;
    }

    void Pull()
    {
        if (isHooked)
        {
           /* if (GetComponent<Weight>().weight >= transform.parent.GetComponent<Weight>().weight)
            {
                direction = (hook.transform.position - transform.position).normalized;
                hookRigidBody.velocity += (Vector2)direction * speed;
                isHeavy = true;
            }
            else
            {
                direction = (transform.position - hook.transform.position).normalized;
                GetComponent<Rigidbody2D>().velocity += (Vector2)direction * speed;
                isHeavy = false;
            }*/

            //IL SE PASSE DES TRUCS
        }
        else
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
            if(hookable.gameObject.CompareTag("Hookable"))
            {
                //S'accrocher à l'objet
                hook.transform.position = hookable.gameObject.transform.position;
                hook.transform.SetParent(hookable.gameObject.transform);
                hookRigidBody.velocity = Vector2.zero;
                hookRigidBody.simulated = false;
                //isHooked = true;
            }

            if(hookable.gameObject.CompareTag("Player") && isPulling)
            {
                hookRigidBody.velocity = Vector2.zero;
                hookRigidBody.simulated = false;
                isThrown = false;
                isPulling = false;
                playerMovement.canMove = true;
                //l'objet hook est accroché
                //ishooked = true
            }
        }

    }
}
