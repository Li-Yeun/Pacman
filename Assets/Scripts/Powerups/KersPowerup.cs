using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KersPowerup : MonoBehaviour
{
    void OnTriggerStay(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Pellet":
                Destroy(col.gameObject);
                break;
            case "Player":
                ScoreCounter fruitscore = FindObjectOfType<ScoreCounter>();
                fruitscore.FruitPoints();
                playerhealth Playerhealth = GameObject.FindObjectOfType<playerhealth>();
                Playerhealth.health++;
                Destroy(gameObject);
                break;
        }
    }
}
