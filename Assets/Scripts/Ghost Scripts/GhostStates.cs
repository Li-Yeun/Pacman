using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStates : MonoBehaviour
{

    private Animator animator;
    public float TimePassed = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("SpookAnimationBlue");
        animator.SetBool("PacmanIsOnTheHunt", false);
    }

    void Update()
    {
        /*  //Debug
        if (Input.GetKeyDown("8"))
        {
            Vulnerable();
        }
        */

        if (animator.GetBool("PacmanIsOnTheHunt"))
        {
            TimePassed += Time.deltaTime;
        }

        if (TimePassed >= 10)
        {
            animator.SetBool("PacmanIsOnTheHunt", false);
            TimePassed = 0;
        }
    }

    public void Vulnerable()
    {
        animator.SetBool("PacmanIsOnTheHunt", true);
    }

    public bool IsVulnerable
    {
        get { return animator.GetBool("PacmanIsOnTheHunt");}
    }


}