using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPacmanMovement : MonoBehaviour
{

    [SerializeField] SpecialTrigger2 trigger;
    PacmanMovement pacmanMove;
    private void Start()
    {
        pacmanMove = FindObjectOfType<PacmanMovement>();
    }

    private void Update()
    {
        if (trigger.Collision)
        {
            pacmanMove.AnimationLock = true;
        }
        else
        {
            pacmanMove.AnimationLock = false;
        }
    }
}
