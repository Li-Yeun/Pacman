using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitroenPowerup : MonoBehaviour
{
    private PacmanMovement pacmanMovement;
    // Use this for initialization

    private void Start()
    {
        PacmanInstantiated();
    }
    void OnTriggerStay(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Pellet":
                Destroy(col.gameObject);
                break;
            case "Player":
                Destroy(gameObject);
                pacmanMovement.Speed.x++;
                pacmanMovement.Speed.z++;
                break;
        }
    }
    public void PacmanInstantiated()
    {
        pacmanMovement = FindObjectOfType<PacmanMovement>();
    }
}