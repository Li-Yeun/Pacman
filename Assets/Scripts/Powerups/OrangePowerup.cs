using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script is responsable for making the blocks transparent when the orange is picked up.
/// It's inefficient as shit but it works.
/// Todo make this beter
/// </summary>
public class OrangePowerup : MonoBehaviour
{
    // Variable for measuring how long the powerup has been running for (in seconds)
    [SerializeField] Material material, defaulthMaterial;

    float passedTime = 0;

    // Constant which holds how long the powerup should last (in seconds) 
    const float powerupDuration = 15;

    // Variable which states if the current powerup is in effect
    bool active = false;

    // Variable used for the opacity of the maze wall
    float transparency = 0.5f;

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
                active = true;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 200f, gameObject.transform.position.z);
                // Loops through all game objects to find those which make up the walls of the maze (excluding the exterior walls of the maze)
                foreach (GameObject Maze in GameObject.FindGameObjectsWithTag("Maze"))
                {
                    
                    if (Maze.GetComponentInChildren<SliderWallScript>()) { continue; }
                    // Creates a variable indicating the preferred alpha of the maze wall color and changes the maze wall color alpha to said variable value
                    Maze.GetComponentInChildren<Renderer>().material = material;
                    Maze.GetComponentInChildren<Renderer>().material.color = new Color(Maze.GetComponentInChildren<Renderer>().material.color.r, Maze.GetComponentInChildren<Renderer>().material.color.g, Maze.GetComponentInChildren<Renderer>().material.color.b,transparency = 0f);
                }
                break;
        }
    }

    private void Update()
    {
        {
            passedTime += Time.deltaTime;

            if (powerupDuration - passedTime < 0)
            {
                active = false;
                foreach (GameObject Maze in GameObject.FindGameObjectsWithTag("Maze"))
                {
                    if (Maze.GetComponentInChildren<SliderWallScript>()) { continue; }
                    // Returns the maze wall's color alpha back to the way it was (1f = full opacity) in the same manner as when the opacity was lowered before
                    Maze.GetComponentInChildren<Renderer>().material = defaulthMaterial;

                }
                Destroy(gameObject);
            }
            // This sedction is for the last five seconds, where it changes the maze wall's opacity every second to create a flashing effect, indicating that time is nearly up
            else if (powerupDuration - passedTime < 5 && powerupDuration - passedTime > 0)
            {
                if ((int)Math.Ceiling(powerupDuration - passedTime) % 2 == 1)
                {
                    foreach (GameObject Maze in GameObject.FindGameObjectsWithTag("Maze"))
                    {
                        if (Maze.GetComponentInChildren<SliderWallScript>()) { continue; }
                        Maze.GetComponentInChildren<Renderer>().material.color = new Color(Maze.GetComponentInChildren<Renderer>().material.color.r, Maze.GetComponentInChildren<Renderer>().material.color.g, Maze.GetComponentInChildren<Renderer>().material.color.b, transparency);
                        transparency = 1.5f;
                    }
                }
                else
                {
                    foreach (GameObject Maze in GameObject.FindGameObjectsWithTag("Maze"))
                    {
                        if (Maze.GetComponentInChildren<SliderWallScript>()) { continue; }
                        Maze.GetComponentInChildren<Renderer>().material.color = new Color(Maze.GetComponentInChildren<Renderer>().material.color.r, Maze.GetComponentInChildren<Renderer>().material.color.g, Maze.GetComponentInChildren<Renderer>().material.color.b, transparency);
                        transparency = 0.5f;

                    }

                }
            }
        }
    }
}