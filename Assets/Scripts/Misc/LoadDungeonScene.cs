using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDungeonScene : MonoBehaviour
{
    public static bool loadDungeon;
    public GameObject transitionPanel;
    private void Update()
    {
        if(loadDungeon)
        {
            SceneManager.LoadScene("DungeonScene");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Player"))
       {
            transitionPanel.GetComponent<Animator>().SetBool("dungeon", true);
       }
    }
}
