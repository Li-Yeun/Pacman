using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpecialTrigger : MonoBehaviour {

    public bool Collision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Maze"|| other.gameObject.tag == "TeleportShit")
        {
            Collision = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Maze" || other.gameObject.tag == "TeleportShit")
        {
            Collision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Maze" || other.gameObject.tag == "TeleportShit")
        {
            Collision = false;
        }
    }
}
