using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnOFFLight : MonoBehaviour {

    Light[] DisableLights;
   // List<Light>EnableLights;

    public void Start()
    {
        DisableLights = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Light>();
    }
    void OnPreRender()
    {
        if(DisableLights == null)
        {
            Debug.Log("No Lights On Pre Render");
            return;
        }
        foreach (Light light in DisableLights)
        {
            light.enabled = true;
        }
/*
        foreach (Light light in EnableLights)
        {
            light.enabled = false;
        }
        */
    }

    void OnPostRender()
    {
        if (DisableLights == null)
        {
            Debug.Log("No Lights On Post Render");
            return;
        }
        foreach (Light light in DisableLights)
        {
            light.enabled = false;
        }

        /*
        foreach (Light light in EnableLights)
        {
            light.enabled = true;
        }
        */
    }
}
