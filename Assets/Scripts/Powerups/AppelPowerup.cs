using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppelPowerup : MonoBehaviour {

    public float duration = 15f;

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
                Destroy(gameObject);
                appelTimer timerAnimation = FindObjectOfType<appelTimer>();
                timerAnimation.AppelTimer();
                break;
        }
    }

}
