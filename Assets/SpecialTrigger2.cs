using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTrigger2 : MonoBehaviour {

    public bool Collision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BoundingWall")
        {
            Collision = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "BoundingWall")
        {
            Collision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BoundingWall")
        {
            Collision = false;
        }
    }
}