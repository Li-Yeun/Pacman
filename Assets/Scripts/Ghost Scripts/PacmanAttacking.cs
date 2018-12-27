using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanAttacking : MonoBehaviour {

    Animator animator;
    public float TimePassed = 0;
    public bool PacmanIsTheBoyInTown;
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

        if (animator.GetBool("PacmanIsOnTheHunt")) { TimePassed += Time.deltaTime; PacmanIsTheBoyInTown = true; }

        if (TimePassed >= 10)
        {
            animator.SetBool("PacmanIsOnTheHunt", false);
            PacmanIsTheBoyInTown = false;
            TimePassed = 0; 
        }
	}

    public void PacmanIsTheHunter()
    {
        animator.SetBool("PacmanIsOnTheHunt", true);
    }
}
