﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeranimation : MonoBehaviour {

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void JumpTimer()
    { anim.SetTrigger("JumpTimer"); }
}
