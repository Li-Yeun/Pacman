using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLight : MonoBehaviour {

    public List<Light> Lights;
    void OnPreRender()
    {
        foreach (Light light in Lights)
        {
            light.enabled = true;
        }
    }

    void OnPostRender()
    {
        foreach (Light light in Lights)
        {
            light.enabled = false;
        }
    }
}

