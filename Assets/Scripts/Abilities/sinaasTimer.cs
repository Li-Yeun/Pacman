using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinaasTimer : MonoBehaviour {

    //starts Orange UI timer
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OrangeTimer()
    { anim.Rebind(); anim.Play("orangetimer"); }
    
}
