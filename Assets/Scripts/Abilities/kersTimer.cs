using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kersTimer : MonoBehaviour {

    //start de kers UI animatie
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void KersTimer()
    {
        anim.Rebind(); anim.Play("kerstimer"); }

}
