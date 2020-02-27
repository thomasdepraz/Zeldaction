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

    [Header("Logic")]
    [Range(0f, 5f)]
    public float speed;
    private Vector3 direction;
    private bool isThrown;
    private Vector3 originPosition;
    private float distancePlayerHook;


    // Start is called before the first frame update
    void Start()
    {
        hookRigidBody = hook.GetComponent<Rigidbody2D>();
        playerAim = gameObject.GetComponent<PlayerAim>();
    }

    // Update is called once per frame
    void Update()
    {
        distancePlayerHook = (gameObject.transform.position - hook.transform.position).magnitude;

        if(Input.GetButtonDown("Throw") && !isThrown)//Si le hameçon n'est pas lancé et qu'on appui sur R1 alors on le lance.
        {
            originPosition = hook.transform.position;
            isThrown = true;
            Throw();
        }

        if(isThrown)
        {
            Hook();
        }

        if(isThrown && distancePlayerHook >= playerAim.maxRange)//Si le hameçon atteint maxRange il s'arrête
        {
            hookRigidBody.velocity = Vector2.zero;
        }

    }

    void Throw()
    {
        direction = (crosshair.transform.position - hook.transform.position).normalized;
        hookRigidBody.velocity += (Vector2)direction * speed; 
    }

    void Hook()
    {
        
    }
}
