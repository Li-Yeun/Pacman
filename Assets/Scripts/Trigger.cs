using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
    public bool Collision;
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Maze")
        {
            Collision = true;
        }


    }

    private void OnTriggerExit(Collider col)
    {
        Collision = false;
    }
}