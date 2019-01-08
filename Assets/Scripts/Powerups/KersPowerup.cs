using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KersPowerup : MonoBehaviour {

    private playerhealth PlayerHealth;

    void OnTriggerStay(Collider col)
    {
        /// <summary>
        /// Checks if the power-up collides with a pellet or with the player
        /// If the power-up collides with a pellet then the pellet is deleted to create space for the power-up
        /// If the power-up collides with the player then the player gets one extra life
        /// </summary>
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
