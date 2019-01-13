using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeloenPowerup : MonoBehaviour {

    public float duration = 15f;

    void OnTriggerStay(Collider col)
    {
        //checks if collides with player or pellet, if collides with player then reverse the controls of the player
        switch (col.gameObject.tag)
        {
            case "Pellet":
                Destroy(col);
                break;
            case "Player":
                gameObject.GetComponent<SphereCollider>().enabled = false;
                gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                PacmanMovement pacmanMovement = col.GetComponent<PacmanMovement>();
                melonTimer timerAnimation = FindObjectOfType<melonTimer>();
                timerAnimation.MelonTimer();
                pacmanMovement.Reversecontrols = true;
                StartCoroutine(Resett(pacmanMovement));
              
                break;
        }
    }

    IEnumerator Resett(PacmanMovement pacmanMovement)
    {
        yield return new WaitForSeconds(duration);
        pacmanMovement.Reversecontrols = false;
        Destroy(gameObject);
    }
}
