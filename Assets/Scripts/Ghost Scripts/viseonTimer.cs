using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viseonTimer : MonoBehaviour {

    //start de increased viseon Timer UI animatie
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ViseonTimer()
    { anim.SetTrigger("ViseonTimer"); }
}
