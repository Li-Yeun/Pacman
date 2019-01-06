using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KersPowerup : MonoBehaviour {

    private playerhealth PlayerHealth;

    void OnTriggerStay(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Pellet":
                Destroy(col.gameObject);
                break;
            case "Player":
                Destroy(gameObject);
                PlayerHealth.IncreaseHealth();
                break;
        }
    }
    public void PacmanInstantiated()
    {
        PlayerHealth = FindObjectOfType<playerhealth>();
    }
}
