﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Play()
    {
        SceneManager.LoadScene("Level-Design_MathieuScene");
    }

    public void Options()
    {

    }

    public void Quit()
    {
        Application.Quit();   
    }

}
