using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanCollision : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                SendMessage("StartDeathSequence");
                break;
            case "Friendly":
                break;
            default:
                break;
        }

    }


}
