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
