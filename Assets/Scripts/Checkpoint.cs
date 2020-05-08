using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject combatEvent;
    private CombatEvent cEvent;
    private GameObject storedCombatEvent;

    private void Start()
    {
        cEvent = combatEvent.GetComponent<CombatEvent>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            PlayerManager.lastCheckpoint = gameObject;

        if(combatEvent != null)
        {
            if(combatEvent.activeSelf)
            {
                storedCombatEvent = combatEvent; 
            }
        }
    }

    public void ResetFight()
    {
        //cEvent.arenaCollider.SetActive(false); //on désactive le collider de l'arène
        Physics2D.IgnoreLayerCollision(10, 16, true);
        cEvent.DeactivateEnemies();
    }
}
