﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallsTimer : MonoBehaviour {

    //start de walking walls Timer UI animatie
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void WallsTimer()
    { anim.SetTrigger("WallsTimer"); }
}

