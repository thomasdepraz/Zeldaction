﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{

    public void GetAnimationEvent(string parameter)
    {
        if(parameter == "loadDungeon")
        {
            LoadDungeonScene.loadDungeon = true;
        }
    }
}
