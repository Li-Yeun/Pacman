using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeranimation : MonoBehaviour {
    //start de jump UI animatie
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void JumpTimer()
    {
        anim.SetTrigger("JumpTimer");
    }
}
