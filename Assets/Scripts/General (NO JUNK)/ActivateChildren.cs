using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateChildren : MonoBehaviour {

    [SerializeField] string name;
    public void Reset()
    {
        gameObject.SetActiveRecursively(true);
        if (name == "Pellets")          // Alle Pellets worden hierdoor gerest zodat ze weer zichtbaar zijn
        {
            foreach (PelletBehaviour pellet in GetComponentsInChildren<PelletBehaviour>())
            {
                pellet.pelleteaten = false;
            }
        }
        else if (name == "PowerPills")  // Alle PowerPills worden hierdoor gerest zodat ze weer zichtbaar zijn
        {
            foreach (PowerpilBehaviour powerPill in GetComponentsInChildren<PowerpilBehaviour>())
            {
                powerPill.powerpileaten = false;
            }
        }
    }
}
