using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kersTimer : MonoBehaviour {

    //start de kers UI animatie
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void KersTimer()
    { anim.SetTrigger("KersTimer"); }

}
