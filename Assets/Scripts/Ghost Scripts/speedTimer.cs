using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedTimer : MonoBehaviour {

    //start de speedTimer UI animatie
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SpeedTimer()
    { anim.SetTrigger("SpeedTimer"); }
}
