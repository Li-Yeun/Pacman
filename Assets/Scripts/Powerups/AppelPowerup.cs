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
                Destroy(gameObject);
                timeranimation timerAnimation = FindObjectOfType<timeranimation>();
                timerAnimation.AppelTimer();
                break;
        }
    }

}
