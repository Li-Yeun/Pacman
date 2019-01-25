using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aardbeiTimer : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void AardbeiTimer()
    { anim.SetTrigger("AardbeiTimer"); }
}
