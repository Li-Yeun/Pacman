using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanAttacking : MonoBehaviour {

    Animator animator;
    float TimePassed = 0;
	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
        animator.Play("SpookAnimationBlue");
        animator.SetBool("PacmanIsOnTheHunt", false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("8"))
        {
            PacmanIsTheHunter();
        }

        if (animator.GetBool("PacmanIsOnTheHunt")) { TimePassed += Time.deltaTime; }

        if (TimePassed >= 10)
        {
            animator.SetBool("PacmanIsOnTheHunt", false);
            TimePassed = 0; 
        }
	}
    public void PacmanIsTheHunter()
    {
        animator.SetBool("PacmanIsOnTheHunt", true);
    }
}
