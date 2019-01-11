﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppelPowerup : MonoBehaviour {

    void OnTriggerStay(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Pellet":
                Destroy(col.gameObject);
                break;
            case "Player":
                Destroy(gameObject);
                break;
        }
    }

}
