using UnityEngine;

public class CombatEvent : MonoBehaviour
{
    public string id;
    public GameObject[] enemies;
    [HideInInspector] public bool combatStarted = false;
    [HideInInspector] public bool combatEnded = false;
    private bool spawnedEnemies = false;
    private BoxCollider2D fightTrigger;
    public GameObject arenaCollider;
    public bool isCombatDialog;

    [Header("Objects to activate / deactivate")]
    public GameObject[] objects;
    public GameObject[] deactivateObjects;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.TryGetComponent<BoxCollider2D>(out BoxCollider2D collider))
        {
            fightTrigger = collider;
        }
        else
        {
            fightTrigger = null;
        }
    }

    void Update()
    {
        if(combatStarted && !spawnedEnemies)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if(enemies[i] != null)
                {
                    enemies[i].SetActive(true);

                }
            }

            arenaCollider.SetActive(true);
            Physics2D.IgnoreLayerCollision(10, 16, false);
            spawnedEnemies = true;
        }
        if(gameObject.transform.childCount == 0 && combatStarted)
        {
            combatStarted = false;
            combatEnded = true;
            arenaCollider.SetActive(false);
            Debug.Log("Activation");
            if (objects != null)
            {
                for (int i = 0; i < objects.Length; i++)
                {
                    objects[i].SetActive(true);
                }
            }
            if (deactivateObjects != null)
            {
                for (int i = 0; i < deactivateObjects.Length; i++)
                {
                    deactivateObjects[i].SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(fightTrigger!=null && !isCombatDialog)
            {
                combatStarted = true;
            }
        }
    }

    public void DeactivateEnemies()
    {
        if(isCombatDialog)
        {
            isCombatDialog = false;
        }
        combatStarted = false;
        spawnedEnemies = false;
    }
}
