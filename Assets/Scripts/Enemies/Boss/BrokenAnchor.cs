using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenAnchor : MonoBehaviour
{
    public Animator anim;
    private void Start()
    {
        anim.SetTrigger("BrokenAnchor");
    }
    public void Anchor()
    {
        GameObject.Find("Boss").GetComponent<BossManager>().AnchorFB();
    }
}
