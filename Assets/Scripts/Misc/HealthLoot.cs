using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLoot : MonoBehaviour
{
    public int healValue;
    string playerTag = "Player";
    private GameObject player;
    private PlayerHP playerHP;
    private bool pickup = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        playerHP = player.GetComponent<PlayerHP>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(playerTag) && gameObject.transform.childCount == 2)
        {
            if(playerHP.currentHealth < playerHP.maxHealth - healValue)
            {
                playerHP.TakeDamage(-healValue);
                pickup = true;
            }

            if(pickup)
            {
                Destroy(gameObject);
            }
           
        }
    }
}
