using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
    public bool Collision;
    private void OnTriggerEnter(Collider col)
    {
        Collision = true;

    }

    private void OnTriggerExit(Collider col)
    {
        Collision = false;
    }
}
