﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletBehaviour : MonoBehaviour {

    public bool pelleteaten;
    // Use this for initialization
    void Start ()
    {
        pelleteaten = false;
    }

    void OnTriggerEnter(Collider other)
    {
        /// <summary>
        /// Checks if the thing colliding with the pellet is Pacman
        /// If Pacman collides with the pellet the pellet gets eaten and disappears from the map and the player is awarded 10 points
        /// If something other than Pacman collides with a pellet nothing happens
        /// </summary>
        if (other.CompareTag("Player") || other.CompareTag("Decoy"))
        {
            pelleteaten = true;
        }
        else if (other.CompareTag("Fruit"))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// Checks if the pellet is already eaten in which case it will be deactivated
    /// </summary>
    void Update () {
		if (pelleteaten)
        {
            if (FindObjectsOfType<ScoreCounter>().Length == 1)
            {
                ScoreCounter scoreCounter = FindObjectOfType<ScoreCounter>();
                scoreCounter.PelletPoints();
                PelletCounter pelletCounter = FindObjectOfType<PelletCounter>();
                pelletCounter.DecreaseCounter();
            }
            gameObject.SetActive(false);
        }
	}
}
