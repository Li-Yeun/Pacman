using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeranimation : MonoBehaviour {

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void MelonTimer()
    { anim.SetTrigger("MelonTimer"); }

    public void AardbeiTimer()
    { anim.SetTrigger("AardbeiTimer"); }

    public void AppelTimer()
    { anim.SetTrigger("AppelTimer"); }

    public void OrangeTimer()
    { anim.SetTrigger("OrangeTimer"); }

    public void JumpTimer()
    { anim.SetTrigger("JumpTimer"); }
}
