﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinaasTimer : MonoBehaviour {

    //starts Orange UI timer
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OrangeTimer()
    { anim.SetTrigger("OrangeTimer"); }
}