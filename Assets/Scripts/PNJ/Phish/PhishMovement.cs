using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhishMovement : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public bool canMove;

    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        canMove = true;
    }
        
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) > stoppingDistance && canMove == true)
        {
            if (gameObject.activeInHierarchy)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.zero;
            }
        }
    }
}
