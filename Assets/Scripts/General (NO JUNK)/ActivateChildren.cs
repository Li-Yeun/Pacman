using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateChildren : MonoBehaviour {

    public void Reset()
    {
        gameObject.SetActiveRecursively(true);
        foreach (PelletBehaviour Pellet in GetComponentsInChildren<PelletBehaviour>())
        {
            Pellet.pelleteaten = false;
        }

    }
}
