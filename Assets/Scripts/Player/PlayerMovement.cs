using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  //Player 
    [Header("Player variables")]
    public Rigidbody2D playerRb;
    public Vector3 playerControl;
    public float playerSpeed = 500;
    private float controllerDeadzone = 0.25f;

    //Player inputs
    [Header("Player inputs")]
    [Range(-1f, 1f)]
    public float playerBindHorizontal;
    [Range(-1f, 1f)]
    public float playerBindVertical;

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
        playerControl = new Vector3(playerBindHorizontal, playerBindVertical);

        if (playerControl.magnitude < controllerDeadzone) // ajout d'une deadzone
        {
            playerControl = Vector2.zero;
            playerRb.velocity = Vector2.zero;
        }
        else
        {
            playerRb.velocity = new Vector2(playerBindHorizontal * playerSpeed * Time.deltaTime, playerBindVertical * playerSpeed * Time.deltaTime);
        }

	}
    void GetPlayerController()
    {
        playerBindHorizontal = Input.GetAxis("Horizontal");
        playerBindVertical = Input.GetAxis("Vertical");

        // fait en sorte que l'angle du player soit celui de la dernière direction choisie
        if (playerControl != Vector3.zero)
        { 
            playerRb.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Vector2.SignedAngle(Vector2.up, playerControl));
        }
    }

}
