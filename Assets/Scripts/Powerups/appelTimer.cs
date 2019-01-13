using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appelTimer : MonoBehaviour {

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void AppelTimer()
    { anim.SetTrigger("AppelTimer"); }
}
