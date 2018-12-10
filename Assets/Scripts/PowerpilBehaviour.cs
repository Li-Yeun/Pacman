﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerpilBehaviour : MonoBehaviour {

    public bool powerpileaten;
    public GameObject powerpil;

	// Use this for initialization
	void Start () {
        powerpileaten = false;
	}

    //Checks if pacman hit the trigger and if this is true removes the powerpil
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EatPowerpil();
        }
    }

    //Confirms powerpil has been eaten
    public bool EatPowerpil()
    {
        return powerpileaten = true;
    }

    // Update is called once per frame
    void Update () {
		if (powerpileaten)
        {
            powerpil.SetActive(false);
        }
	}
}
