using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateform : MonoBehaviour
{
    public GameObject player;

    [Range(0f, 5f)]
    public float speed;
    public Transform[] position;
    private int index = 0;

    Vector3 nextPos;

    void Start()
    {
        nextPos = position[index].position;
    }

    void Update()
    {
        if(index < position.Length)
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
    }

    private void OnDrawGizmos()
    {
        for(int i=0; i < position.Length -1; i++)
        {
            Gizmos.DrawLine(position[i].position, position[i + 1].position);
        }
    }

    // Le PJ est parenté par les déplacements de la plateform lorsqu'il se trouve dessus. 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("dans le radeau");
            player.transform.parent = gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("hors du radeau");
            player.transform.parent = null;
        }
    }
}
