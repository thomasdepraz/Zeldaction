using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    Rigidbody2D rb;
    float attackRange;
    void Start()
    {
        attackRange = GetComponent<Claw_Attack>().attackRange;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Phase1()
    {
        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            ;
        }
    }
    public void Phase2()
    {

    }
}
//je veux que si le joueur n'est pas dans la zone d'attaque depuis un certain temps (zone d'attaque non déclenchée ou distance?), le boss invoque des crabes de base toutes les 5 secondes près du joueur tant qu'il y a moins de deux crabes.