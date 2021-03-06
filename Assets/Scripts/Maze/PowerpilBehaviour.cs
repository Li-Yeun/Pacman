﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Makes the powerpil disappear from the map when Pacman eats it
/// Triggers the event that Pacman can eat the ghosts instead of the other way around
/// </summary>
public class PowerpilBehaviour : MonoBehaviour {

    [SerializeField] public bool powerpileaten;
    private GhostStates[] AnimationScriptSpookjes;

    // Use this for initialization
    void Start ()
    {
        powerpileaten = false;
	}

    //Checks if pacman hit the trigger and if this is true removes the powerpil
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            powerpileaten = true;
        }
    }

    // Update is called once per frame
    void Update () {
		if (powerpileaten)
        {
            gameObject.SetActive(false);

            if(AnimationScriptSpookjes == null)
            {
                Debug.Log("No Ghosts");
                return;
            }
            foreach (GhostStates Ghost in AnimationScriptSpookjes)
            {
                Ghost.Vulnerable();
            }
        }   
	}

    public void GhostInstantiated()
    {
        AnimationScriptSpookjes = FindObjectsOfType<GhostStates>();
    }

}
