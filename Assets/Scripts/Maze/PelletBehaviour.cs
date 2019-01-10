﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletBehaviour : MonoBehaviour {

    public bool pelleteaten;
    private ScoreCounter pelletscore;
    public float Timer = 0;


    // Use this for initialization
    void Start ()
    {
        pelletscore = FindObjectOfType<ScoreCounter>();
        pelleteaten = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        /// <summary>
        /// Checks if the thing colliding with the pellet is Pacman
        /// If Pacman collides with the pellet the pellet gets eaten and disappears from the map and the player is awarded 10 points
        /// If something other than Pacman collides with a pellet nothing happens
        /// </summary>
        if (other.tag == "Player")
        {
            EatPellet();
        }
    }

    // Confirms the pellet has been eaten
    public bool EatPellet()
    {
        return pelleteaten = true;
    }

    /// <summary>
    /// Update is called once per frame
    /// Checks if the pellet is already eaten in which case it will be deactivated
    /// </summary>
    void Update () {
		if (pelleteaten)
        {
            Destroy(gameObject);
            pelletscore.PelletPoints();
        }
	}
}
