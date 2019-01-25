using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTrigger2 : MonoBehaviour {

    [SerializeField] string Firststring = "Maze";
    [SerializeField] string Secondstring = "BoundingWall";
    public bool Collision;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Firststring || other.gameObject.tag == Secondstring)
        {
            Collision = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == Firststring || other.gameObject.tag == Secondstring)
        {
            Collision = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Firststring || other.gameObject.tag == Secondstring)
        {
            Collision = false;
        }
    }
}