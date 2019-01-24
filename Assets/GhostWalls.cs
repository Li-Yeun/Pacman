using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWalls : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Maze"))
        {
            if (GetComponentInParent<WalkThroughWalls>().GhostWalking)
            {
                other.GetComponentInChildren<BoxCollider>().isTrigger = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Maze"))
        {
            if (GetComponentInParent<WalkThroughWalls>().GhostWalking)
            {
                other.GetComponentInChildren<BoxCollider>().isTrigger = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Maze"))
        {
            other.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
