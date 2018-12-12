using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderWallScript : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject pacmanMovement;
    float TimeSpent = 0;
    bool DoorOpen = false;
    void Start()
    {
        pacmanMovement = FindObjectOfType<PacmanMovement>().gameObject;
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
            else if (DoorOpen && ( Mathf.Abs(transform.position.x - pacmanMovement.transform.position.x) >= 3 || Mathf.Abs(transform.position.z - pacmanMovement.transform.position.z) >= 3))
            {
                DoorOpen = false;
                anim.Play("SlidingOut");
                TimeSpent = 0;
            }
        }
    }
}
