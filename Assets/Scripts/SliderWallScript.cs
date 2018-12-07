using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderWallScript : MonoBehaviour
{

    // Use this for initialization
    public Animator anim;
    public PacmanMovement pacmanMovement;
    float TimeSpent = 0;
    bool DoorOpen = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("SlidingOut");
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpent += Time.deltaTime;
        if (TimeSpent >= 5)
        {
            if (Input.GetKeyDown("1"))
            {
                anim.Play("SlidingIn");
                DoorOpen = true;
                TimeSpent = 0;
            }
            else if (DoorOpen && ( Mathf.Abs(transform.position.x - pacmanMovement.PacmanPos.x) >= 3 || Mathf.Abs(transform.position.z - pacmanMovement.PacmanPos.z) >= 3))
            {
                DoorOpen = false;
                anim.Play("SlidingOut");
                TimeSpent = 0;
            }
        }
    }
}
