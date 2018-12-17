using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
    public bool Collision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Maze")
        {
            Collision = true;
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Maze")
        {
            Collision = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Maze")
        {
            Collision = false;
        }
    }
}