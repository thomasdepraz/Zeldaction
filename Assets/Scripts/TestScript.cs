using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    //Elements
    [Header ("Elements")]
    public Animator anim;
    public Rigidbody2D rb;
    [Tooltip ("This is a collider")]
    public Collider2D col;


    //Variable
    [Header("Tweaks")]
    [Range (0f,5f)]
    public float speed;
    [Range(0f, 5f)]
    public float weight;

    

    
    
    void Awake()
    {
        
    }
    
    void Start()
    {
   
    }
    
    void Update()
    {
        
    }
    
    
    
}
