using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinaasTimer : MonoBehaviour {

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OrangeTimer()
    { anim.SetTrigger("OrangeTimer"); }
}
