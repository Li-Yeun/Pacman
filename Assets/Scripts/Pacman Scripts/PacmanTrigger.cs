using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanTrigger : MonoBehaviour {

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
        if (col.gameObject.tag == "Maze")
        {
            Collision = false;
        }
    }
}
