using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melonTimer : MonoBehaviour {

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void MelonTimer()
    { anim.SetTrigger("MelonTimer"); }
}
