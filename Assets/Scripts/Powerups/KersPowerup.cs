using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KersPowerup : MonoBehaviour {

    public playerhealth PlayerHealth;

	// Use this for initialization
	void Start () {
        PlayerHealth = FindObjectOfType<playerhealth>();
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
                PlayerHealth.IncreaseHealth();
                break;
        }
    }

    void SameFruitChecker()
    {
        foreach (KersPowerup kp in FindObjectsOfType<KersPowerup>())
        {
            if (kp.transform.position.x == gameObject.transform.position.x && kp.transform.position.y == gameObject.transform.position.y && kp != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
