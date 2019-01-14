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
                ScoreCounter fruitscore = FindObjectOfType<ScoreCounter>();
                fruitscore.FruitPoints();
                gameObject.GetComponent<SphereCollider>().enabled = false;
                gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                Movement GhostMovement = col.GetComponent<Movement>();
                melonTimer timerAnimation = FindObjectOfType<melonTimer>();
                timerAnimation.MelonTimer();
                GhostMovement.reversecontrols = true;
                StartCoroutine(Resett(GhostMovement));
              
                break;
        }
    }

    IEnumerator Resett(Movement GhostMovement)
    {
        yield return new WaitForSeconds(duration);
        GhostMovement.reversecontrols = false;
        Destroy(gameObject);
    }
}
