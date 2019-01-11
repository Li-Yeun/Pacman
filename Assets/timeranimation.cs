using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeranimation : MonoBehaviour {

    public Animator anim;
    void start()
    {
        anim = GetComponent<Animator>();
    }

    public void update()
    {
        anim.SetTrigger("Timer");
    }
}
