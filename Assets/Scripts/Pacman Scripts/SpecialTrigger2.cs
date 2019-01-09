using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTrigger2 : MonoBehaviour {

    public bool Collision;
    [SerializeField] string Firststring = "Maze";
    [SerializeField] string Secondstring = "BoundingWall";
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Firststring || other.gameObject.tag == Secondstring)
        {
            Collision = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == Firststring || other.gameObject.tag == Secondstring)
        {
            Collision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Firststring || other.gameObject.tag == Secondstring)
        {
            Collision = false;
        }
    }
}