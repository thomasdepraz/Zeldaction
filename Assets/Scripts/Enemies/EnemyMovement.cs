using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D enemyRb;
    public Vector2 direction;
    public float enemySpeed = 150f;
    public bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (canMove)
        {
            followPlayer();
        }
    }

    void followPlayer()
    {
        direction = (player.transform.position - transform.position);
        enemyRb.velocity = direction.normalized * enemySpeed * Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hello There");
        enemyRb.constraints = RigidbodyConstraints2D.FreezeAll; // lui freeze sa position
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        enemyRb.constraints = RigidbodyConstraints2D.None; //permet à l'ennemi de ne plus être freeze lorsque le joueur sort de son collider
    }
}
