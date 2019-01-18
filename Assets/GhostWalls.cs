using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWalls : MonoBehaviour
{
    public bool GhostWalking = false;
    public bool GhostWalkingCD = false;
    public float GhostWalkingDuration = 2f;
    public float GhostWalkingCDDuration = 15f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !GhostWalkingCD && gameObject.GetComponentInParent<Movement>().name == "Pink")
        {
            GhostWalking = true;
            GhostWalkingCD = true;
            Invoke("StopGhostWalking", GhostWalkingDuration);
            Invoke("CDduration", GhostWalkingCDDuration);
            wallsTimer timerAnimation = FindObjectOfType<wallsTimer>();
            timerAnimation.WallsTimer();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Maze"))
        {
            if (GhostWalking)
            {
                other.GetComponentInChildren<BoxCollider>().isTrigger = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Maze"))
        {
            if (GhostWalking)
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
    private void StopGhostWalking()
    {
        GhostWalking = false;
    }
    private void CDduration()
    {
        GhostWalkingCD = false;
    }
}
