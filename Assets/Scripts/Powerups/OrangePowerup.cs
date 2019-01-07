using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class OrangePowerup : MonoBehaviour
{
    // Variable for measuring how long the powerup has been running for (in seconds)
    float passedTime = 0;

    // Constant which holds how long the powerup should last (in seconds) 
    const float powerupDuration = 15;

    // Variable which states if the current powerup is in effect
    bool active = false;

    // Variable used for the opacity of the maze wall
    const float transparency = 0.5f;

    void OnTriggerStay(Collider col)
    {
        // Changes the flow of control based on the object with which the orange collides
        switch (col.gameObject.tag)
        {
            case "Pellet":
                // Destroys the pallet so that the orange can take its place without them overlapping
                Destroy(col.gameObject);
                break;
            case "Player":
                // Destroys the orange so that the same orange cannot be used multiple times
                Destroy(gameObject);
                // Loops through all game objects to find those which make up the walls of the maze (excluding the exterior walls of the maze)
                foreach (GameObject Maze in GameObject.FindGameObjectsWithTag("Maze"))
                {
                    // Creates a variable indicating the preferred alpha of the maze wall color and changes the maze wall color alpha to said variable value
                    var color = Maze.GetComponent<Renderer>().material.color;
                    color.a = transparency;
                }
                active = true;
                break;
        }
    }

    private void Update()
    {
        if (active)
        {
            passedTime += Time.deltaTime;

            if(powerupDuration - passedTime < 0)
            {
                active = false;
                foreach (GameObject Maze in GameObject.FindGameObjectsWithTag("Maze"))
                {
                    // Returns the maze wall's color alpha back to the way it was (1f = full opacity) in the same manner as when the opacity was lowered before
                    var color = Maze.GetComponent<Renderer>().material.color;
                    color.a = 1f;
                }
            }
            // This sedction is for the last five seconds, where it changes the maze wall's opacity every second to create a flashing effect, indicating that time is nearly up
            else if(powerupDuration - passedTime < 5 && powerupDuration - passedTime > 0)
            {
                if((int)Math.Ceiling(powerupDuration - passedTime) % 2 == 1)
                {
                    foreach (GameObject Maze in GameObject.FindGameObjectsWithTag("Maze"))
                    {
                        var color = Maze.GetComponent<Renderer>().material.color;
                        color.a = transparency * 1.5f;
                    }
                }else{
                    foreach (GameObject Maze in GameObject.FindGameObjectsWithTag("Maze"))
                    {
                        var color = Maze.GetComponent<Renderer>().material.color;
                        color.a = transparency;
                    }
                }
            }
        }
    }
}
