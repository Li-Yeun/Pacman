using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateChildren : MonoBehaviour {

    [SerializeField] string name;
    public void Reset()
    {
        gameObject.SetActiveRecursively(true);
        if (name == "Pellets")
        {
            foreach (PelletBehaviour pellet in GetComponentsInChildren<PelletBehaviour>())
            {
                pellet.pelleteaten = false;
            }
        }
        else if (name == "PowerPills")
        {
            foreach (PowerpilBehaviour powerPill in GetComponentsInChildren<PowerpilBehaviour>())
            {
                powerPill.powerpileaten = false;
            }
        }
    }
}
