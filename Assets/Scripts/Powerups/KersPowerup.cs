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
                playerhealth Playerhealth = GameObject.FindObjectOfType<playerhealth>();
                Playerhealth.health++;
                kersTimer timerAnimation = FindObjectOfType<kersTimer>();
                timerAnimation.KersTimer();
                Destroy(gameObject);
                break;
        }
    }
}
