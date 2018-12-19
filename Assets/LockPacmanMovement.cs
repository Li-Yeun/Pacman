using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPacmanMovement : MonoBehaviour
{

    [SerializeField] SpecialTrigger2 trigger;
    PacmanMovement pacmanMovement;
    private void Start()
    {
        pacmanMovement = FindObjectOfType<PacmanMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pacmanMovement.Teleporterlock = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pacmanMovement.Teleporterlock = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pacmanMovement.Teleporterlock = false;
        }
    }
}
