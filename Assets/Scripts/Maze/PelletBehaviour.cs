using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletBehaviour : MonoBehaviour {

    public bool pelleteaten;
    public float Timer = 0;

    // Use this for initialization
    void Start ()
    {
        
        pelleteaten = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        /// <summary>
        /// Checks if the thing colliding with the pellet is Pacman
        /// If Pacman collides with the pellet the pellet gets eaten and disappears from the map and the player is awarded 10 points
        /// If something other than Pacman collides with a pellet nothing happens
        /// </summary>
        if (other.CompareTag("Player"))
        {
            EatPellet();
        }
        else if (other.CompareTag("Fruit"))
        {
            Destroy(gameObject);
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
            FindObjectOfType<ScoreCounter>().PelletPoints();
            gameObject.SetActive(false);  //niet destroyen is belangrijkr voor de reset!
        }
	}
}
