using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The camera that this script is attachted too, will not render certain light sources.
public class TurnOnOFFLight : MonoBehaviour { 

    Light[] DisableLights;    // The light sources that the camera doesn't render

    public void Start()
    {
        // Find all the lights that you don't want to render on this camera
        if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
        {
            DisableLights = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Light>();
        }
    }
    void OnPreRender()
    {
        if(DisableLights == null)       // Error prevention for null reference
        {
            return;
        }
        foreach (Light light in DisableLights)
        {
            if (light.name == "PacMan Body Light")
                light.enabled = true;
        }
    }

    //This method disables the rendering of certain objects
    void OnPostRender()
    {
        if (DisableLights == null)      // Error prevention for null reference
        {
            return;
        }
        else
        {
            foreach (Light light in DisableLights)
            {
                if(light.name == "PacMan Body Light")
                light.enabled = false;
            }
        }
    }
    public void PacmanInstantiated()
    {
        DisableLights = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Light>();
    }
}
