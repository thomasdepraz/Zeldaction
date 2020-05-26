using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateform : MonoBehaviour
{
    private GameObject player;

    [Range(0f, 5f)]
    public float maxSpeed;
    private float speed;
    public Transform[] position;
    private int index = 0;

    Vector3 nextPos;

    public bool firstRaft = false;
    private bool startedMoving = false;
    private bool onRaft = false;
    public GameObject pillar;
    public GameObject col;
    public GameObject particles;
    

    void Start()
    {
        nextPos = position[index].position;
        player = GameObject.FindGameObjectWithTag("Player");
        if(firstRaft)
        {
            speed = 0;
        }
        else
        {
            speed = maxSpeed;
            particles.SetActive(true);
        }
    }

    void Update()
    {
        if(position != null)
        {
            if (index <= position.Length - 1)
            {
                if (transform.position == nextPos)
                {
                    index++;
                    nextPos = position[index].position;
                }

                transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
            }
            else
            {
                index = -1;
            }

            if (pillar.transform.childCount > 0 && !onRaft)
            {
                Debug.Log("je me suis arreté");
                speed = 0;
            }
            else
            {
                if (startedMoving)
                {
                    speed = maxSpeed;
                }
            }
        
            if(onRaft)
            {
                speed = maxSpeed;
            }

        }
        

    }

 

    // Le PJ est parenté par les déplacements de la plateform lorsqu'il se trouve dessus. 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.parent = gameObject.transform;
            if(firstRaft)
            {
                speed = maxSpeed;
                startedMoving = true;
                particles.SetActive(true);
            }
            onRaft = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.parent = null;
        }
        onRaft = false;
    }
}
