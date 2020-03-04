using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  //Player 
    [Header("Player variables")]
    public Rigidbody2D playerRb;
    public Vector2 direction;
    public float playerSpeed = 500;
    private float controllerDeadzone = 0.25f;

    //Player inputs
    [Header("Player inputs")]
    [Range(-1f, 1f)]
    public float horizontalDir;
    [Range(-1f, 1f)]
    public float verticalDir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerController();
    }

    // Fixed update
    void FixedUpdate()
    {
        Movement();
	}

    void Movement()
    {
        direction = new Vector2(horizontalDir, verticalDir);

        if (direction.magnitude <= controllerDeadzone) // ajout d'une deadzone --> nice mais on peut le faire dans les settings unity
        {
            direction = Vector2.zero;
            playerRb.velocity = Vector2.zero;
        }
        else
        {
            //playerRb.velocity = new Vector2(playerBindHorizontal * playerSpeed * Time.deltaTime, playerBindVertical * playerSpeed * Time.deltaTime); Calcul trop compliqué et pas normalisé
            playerRb.velocity = direction.normalized * playerSpeed * Time.deltaTime;
        }

	}
    void GetPlayerController()
    {
        horizontalDir = Input.GetAxis("Horizontal");
        verticalDir = Input.GetAxis("Vertical");

        // fait en sorte que l'angle du player soit celui de la dernière direction choisie
        if (direction!= Vector2.zero)
        { 
            //playerRb.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Vector2.SignedAngle(Vector2.up, direction)); c'est cool mais pas utile, à voir.
        }
    }

}
