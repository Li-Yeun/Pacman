using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viseonTimer : MonoBehaviour {

    //start de increased viseon Timer UI animatie
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ViseonTimer()
    { anim.Rebind(); anim.Play("skilltimer_white"); }
}
