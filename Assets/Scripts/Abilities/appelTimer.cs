using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appelTimer : MonoBehaviour {

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void AppelTimer()
    {
        anim.Rebind();
        anim.Play("appeltimer"); }
}
