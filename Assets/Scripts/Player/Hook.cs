using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UnityEditor;

public class Hook : Singleton<Hook>
{

    [Header("Elements")]
    private GameObject player;
    private Rigidbody2D playerRb;
    public GameObject hook;
    private SpriteRenderer hookSpr;
    private Rigidbody2D hookRigidBody;
    public GameObject crosshair;
    private PlayerAim playerAim;
    public LineRenderer hookLine;
    public BoxCollider2D playerHitBox;
    public PlayerAudioManager playerAudio;
    public ParticleSystem hookHitParticle;
    private Hookable currentHookable;
    private GameObject hookedObjectparent;

    [Header("Logic")]
    [Range(0f, 10f)]
    public float speed;
    private Vector3 direction;
    [HideInInspector] public bool isThrown = false;
    [HideInInspector] public bool isHooked = false;
    [HideInInspector] public bool isPulling = false;
    private bool canStartCoroutine = true;
    private bool canStartCoroutineHitbox = true;
    private ContactFilter2D hookableFilter;
    private float objectDrag;
    private ContactPoint2D[] hookContacts = new ContactPoint2D[10];
    private ContactPoint2D[] playerContacts = new ContactPoint2D[10];
    private bool canStartUnhook = true;
    private bool canHook = true;
    private bool backToPlayer;
    private Vector2 storedVelocity;

    [Header("Tweak")]
    [Range(0f, 5f)]
    public float hookDetectionRange;
    [Range(0f, 5f)]
    public float timeToCancel = 3f;
    [Range(0f, 15f)]
    public float hookRange;

    private void Awake()
    {
        MakeSingleton(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.Instance.gameObject;
        hookRigidBody = GetComponent<Rigidbody2D>();
        //playerMovement = player.GetComponent<PlayerMovement>();
        playerAim = PlayerManager.Instance.playerAim;
        playerRb = player.GetComponent<Rigidbody2D>();
        hookSpr = hook.GetComponent<SpriteRenderer>();

        hookableFilter.useTriggers = true;
    }

    private void Update()
    {
        if (PlayerManager.hasHook && PlayerManager.useHook)
        {
            #region BasicActions
            if (Input.GetButtonDown("Throw") && !isThrown && !isHooked && playerAim.isAiming)//Si le hameçon n'est pas lancé et qu'on appui sur R1 alors on le lance.
            {
                Throw();
            }
            else if (Input.GetButtonDown("Throw") && isThrown && !isPulling)// si l'hameçon est lancée et qu'on appuie sur R1 on le tire
            {     
                Pull();
            }

            if (isThrown) //Quand le hameçon est lancé on vérifie s'il peut se hook
            {
                HookObject();
                hookLine.SetPosition(0, player.transform.position);
                hookLine.SetPosition(1, transform.position);
            }

            if ((player.transform.position - transform.position).sqrMagnitude > hookRange && isThrown)
            {
                Pull();
            }
            #endregion

            #region HookRenderer
            if (playerAim.isAiming)
            {
                hookSpr.enabled = true;
            }
            else if(!playerAim.isAiming)
            {
                hookSpr.enabled = false;
            }
            #endregion

            #region Particles
            if (isThrown && !isPulling)
            {
                if (hookRigidBody.GetContacts(hookContacts) >= 1 && transform.parent == null)
                {
                    hookHitParticle.Play();
                    Pull();
                }
            }
            #endregion

            #region UI
            /*if(!hookUI.gameObject.activeSelf)
        {
            if(!PlayerManager.hasHook)
            {
                hookUI.gameObject.SetActive(false);
            }
            else
            {
                hookUI.gameObject.SetActive(true);
            }
        }*/
            #endregion

        }
    }

    public void Throw()
    {
        GetComponent<BoxCollider2D>().isTrigger = false;
        isThrown = true;
        backToPlayer = false;

        hookRigidBody.simulated = true;
        hook.transform.SetParent(null);
        direction = (crosshair.transform.position - hook.transform.position).normalized; //direction player -> crosshair
        hookRigidBody.velocity += (Vector2)direction * speed; //déplacement du hook

        //UI and sound in HookThrow
    }

    public void Pull()
    {
        isPulling = true;
        GetComponent<BoxCollider2D>().isTrigger = true;
        StartCoroutine(UnHook());
        if (isHooked)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
            if (isHooked && currentHookable.isActive)
            {
                if(currentHookable.isLight)
                {
                    if(canStartCoroutine)
                    {
                        StartCoroutine(PullingHook());
                    }
                }
                if(currentHookable.isHeavy)
                {
                    if(canStartCoroutine)
                    {
                        PlayerManager.canMove = false;
                        StartCoroutine(PullingPlayer());
                    }
                }
            }
        }
        else
        {
            if(canStartCoroutine)
            {
                StartCoroutine(PullingHook());
            }
        }
    }

    public void HookObject()
    {
        #region HookObjects
        //Hook Objects
        if (canHook)
        {
            List<Collider2D> hitHookables = new List<Collider2D>();
            Physics2D.OverlapCollider(hook.GetComponent<BoxCollider2D>(), hookableFilter, hitHookables);
            foreach (Collider2D hookable in hitHookables)
            {
                if (hookable.gameObject.CompareTag("Hookable") && !isHooked && isThrown)
                {
                    currentHookable = hookable.gameObject.GetComponent<Hookable>();
                    if (currentHookable.isActive)
                    {  
                        playerAudio.PlayClip(playerAudio.hookSoundSource, playerAudio.onHook, 1, playerAudio.hook);

                        if(currentHookable.gameObject.transform.parent != null)//save object parent
                        {
                            hookedObjectparent = currentHookable.gameObject.transform.parent.gameObject;
                        }
                        
                        hook.transform.position = currentHookable.gameObject.transform.position;
                        currentHookable.gameObject.transform.SetParent(transform);
                        currentHookable.GetComponent<Rigidbody2D>().simulated = false;
                        hookRigidBody.velocity = Vector2.zero;
                        //hookRigidBody.simulated = false;
                        isHooked = true;

                        if (isPulling)
                        {
                            StopAllCoroutines();
                            canStartCoroutine = true;
                            Pull();
                        }

                        //if (canStartCoroutine)
                            //StartCoroutine("HookCancel");
                    }
                }
            }
        }
        #endregion

        #region HookPlayer
        //HookPlayer
        if (isPulling && (player.transform.position - transform.position).magnitude < hookDetectionRange)
        {
            if(isHooked)
            {
                ResetParent(currentHookable.gameObject);
                currentHookable.GetComponent<Rigidbody2D>().velocity = storedVelocity;
                isHooked = false;
                //Ignore Layer Collisions
            }
            
            hookRigidBody.velocity = Vector2.zero;
            hookRigidBody.simulated = false;
            transform.position = player.transform.position;//Apres la main
            transform.SetParent(player.transform);
            isPulling = false;
            canHook = true;
            isThrown = false;
            if(PlayerManager.canMove == false)
            {
                PlayerManager.canMove = true;
            }
            backToPlayer = true;
            canStartCoroutine = true;

            //cancel unhook coroutine
            StopCoroutine(UnHook());
            canStartUnhook = true;
        }
        #endregion
    }

    #region PullCoroutines
    private IEnumerator PullingHook()
    {
        canStartCoroutine = false;
        while(!backToPlayer)
        {
            yield return null;
            if ((player.transform.position - transform.position).magnitude > hookDetectionRange)
            {
                direction = (player.transform.position - transform.position);
                hookRigidBody.velocity = direction.normalized * speed * 2;
                storedVelocity = direction.normalized * speed * 2;
            }
        }   
    }

    private IEnumerator PullingPlayer()
    {
        canStartCoroutine = false;
        while (!backToPlayer)
        {
            yield return null;
            if ((player.transform.position - transform.position).magnitude > hookDetectionRange)
            {
                direction = (transform.position - player.transform.position);
                playerRb.velocity = direction.normalized * speed * 2;
            }
        }
    }
    #endregion

    #region CancelCoroutines
    private IEnumerator UnHook()
    {
        canStartUnhook = false;
        yield return new WaitForSeconds(0.6f);
        if (isPulling && isHooked)
        {
            canHook = false;
            isHooked = false;
            ResetParent(currentHookable.gameObject);
            Pull();
        }
        canStartUnhook = true;
    }


    #endregion

    #region Utilities
    private void ResetParent(GameObject hookable)
    {
        if (hookedObjectparent != null)
        {
            hookable.transform.SetParent(hookedObjectparent.transform);
        }
        else
        {
            hookable.transform.SetParent(null);
        }

        hookable.GetComponent<Rigidbody2D>().simulated = true;
    }
    #endregion

}
