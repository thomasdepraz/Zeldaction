using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    [Range(0, 20)]
    public int maxHealth = 10;
    public int currentHealth;
    bool isPhase1;
    //bool isPhase2;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        isPhase1 = GetComponent<BossManager>().isPhase1;
        //isPhase2 = GetComponent<BossManager>().isPhase2;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // le montant des dommages va être soustrait à la vie actuelle de l'ennemi
        StartCoroutine (DamageFB());
        if (currentHealth <= 0 && isPhase1 == true)
        {
            gameObject.GetComponent<BossManager>().Phase2();
            currentHealth = maxHealth;
        }
      /*  else if (currentHealth <= 0 && isPhase2 == true)
        {

        }*/
    }
    IEnumerator DamageFB ()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
