using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player 
    [Header("Player variables")]
    public Rigidbody2D playerRb;
    public Vector2 direction;
    public float headingAngle;
    public float realAngle;

    // public Animator anim; 
    public float playerSpeed = 200f;
    private readonly float controllerDeadzone = 0.25f;
    [HideInInspector]public bool canMove = true;

    //Player inputs
    private float horizontalDir;
    private float verticalDir;

    [Header("Attack Points")]
    public Transform attackPoint;

    // Update is called once per frame
    void Update()
    {
        GetPlayerController();
        Facing();
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
            }
            else
            {
                playerRb.velocity = direction.normalized * playerSpeed * Time.deltaTime;
            }

        }
    }
    void GetPlayerController()
    {
        horizontalDir = Input.GetAxis("Horizontal");
        verticalDir = Input.GetAxis("Vertical");

        /*   anim.SetFloat("HorizontalMove", horizontalDir);
           anim.SetFloat("VerticalMove", verticalDir);*/

        /*   if (direction!= Vector2.zero)
           { 
               playerRb.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Vector2.SignedAngle(Vector2.up, direction)); c'est cool mais pas utile, à voir.
           }
        */
    }
    void Facing()
    {
        headingAngle = Vector2.SignedAngle(Vector2.up, direction);
        attackPoint.localEulerAngles = new Vector3(0.0f, 0.0f, realAngle);

        //changer le realangle que si la longueur du vecteur est supérieure à zéro
        if (direction.magnitude > 0.2f)
        {
            if (headingAngle >= -45f && headingAngle <= 45f)
                realAngle = 0f;

            else if (headingAngle >= 45f && headingAngle <= 135f)
                realAngle = 90f;

            
            else if (headingAngle >= -135f && headingAngle <= -45f)           
                realAngle = -90f;
            
            else            
                realAngle = 180f;
            
        }
    }
}
