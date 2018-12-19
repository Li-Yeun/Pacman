using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitroenPowerup : MonoBehaviour
{
    public PacmanMovement pacmanMovement;
    // Use this for initialization

    void Start()
    {
        pacmanMovement = FindObjectOfType<PacmanMovement>();
        SameFruitChecker();
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
    /// <summary>
    /// prevents multiple of the sameFruit spawning on the same place.
    /// </summary>
    void SameFruitChecker()
    {
        foreach (CitroenPowerup cp in FindObjectsOfType<CitroenPowerup>())
        {
            if (cp.transform.position.x == gameObject.transform.position.x && cp.transform.position.y == gameObject.transform.position.y && cp != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
