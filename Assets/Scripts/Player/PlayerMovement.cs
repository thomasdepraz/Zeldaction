﻿using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    //Player 
    [Header("Player variables")]
    public Rigidbody2D playerRb;
    public Vector2 direction;
    public float headingAngle;
    public float realAngle;

    public Animator animator; //POUR LES GA
    public bool isMoving  = false; //POUR LES GA
    public float playerSpeed = 200f;
    private readonly float controllerDeadzone = 0.25f;
    [HideInInspector]public bool canMove = true;

    //Player inputs
    private float horizontalDir;
    private float verticalDir;
    private float horizontalOrientation;
    private float verticalOrientation;

    [Header("Attack Points")]
    public Transform attackPoint;

    //Particle System
    public GameObject particles;
    public ParticleSystem.MainModule stepParticlesMain;
    public Tilemap groundTiles;
    private Color stepParticleColor;
    private bool getColor = false;

    private void Start()
    {
        stepParticlesMain = particles.GetComponent<ParticleSystem>().main;
       
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerController();
        Facing();

        if(particles.activeSelf)
        {
            Vector3 tileToPlayer = transform.position - groundTiles.gameObject.transform.position;
            Sprite currentSprite = groundTiles.GetSprite(Vector3Int.FloorToInt(tileToPlayer));
            Texture2D texture = currentSprite.texture;
            stepParticleColor = texture.GetPixel((int)currentSprite.pivot.x, (int)currentSprite.pivot.y);

            stepParticlesMain.startColor = stepParticleColor;
        }
    }


    // Fixed update
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        direction = new Vector2(horizontalDir, verticalDir);
        if(canMove)
        {
            if (direction.magnitude <= controllerDeadzone)
            {
                direction = Vector2.zero;
                playerRb.velocity = Vector2.zero;
                isMoving = false;
                particles.SetActive(false);
            }
            else
            {
                playerRb.velocity = direction.normalized * playerSpeed * Time.deltaTime;
                isMoving = true;
                particles.SetActive(true);
            }

        }
    }
    void GetPlayerController()
    {
        horizontalDir = Input.GetAxis("Horizontal");
        verticalDir = Input.GetAxis("Vertical");

        //ANIMATOR POUR LES GA--------------------------------
        animator.SetFloat("HorizontalMove", horizontalDir);
        animator.SetFloat("VerticalMove", verticalDir);

        animator.SetFloat("HorizontalOrientation", horizontalOrientation);
        animator.SetFloat("VerticalOrientation", verticalOrientation);

        animator.SetBool("isMoving", isMoving);
        //----------------------------------------------------
    }
    void Facing()
    {
        headingAngle = Vector2.SignedAngle(Vector2.up, direction);
        attackPoint.localEulerAngles = new Vector3(0.0f, 0.0f, realAngle);

        //changer le realangle que si la longueur du vecteur est supérieure à zéro
        if (direction.magnitude > 0.2f)
        {
            if (headingAngle >= -45f && headingAngle <= 45f)
            {
                realAngle = 0f;
                horizontalOrientation = 0;
                verticalOrientation = 1;
            }

            else if (headingAngle >= 45f && headingAngle <= 135f)
            {
                realAngle = 90f;
                horizontalOrientation = -1;
                verticalOrientation = 0;
            }

            else if (headingAngle >= -135f && headingAngle <= -45f)
            {
                realAngle = -90f;
                horizontalOrientation = 1;
                verticalOrientation = 0;
            }

            else
            {
                horizontalOrientation = 0;
                verticalOrientation = -1;
                realAngle = 180f;
            }    
        }
    }
}
