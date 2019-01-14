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
                foreach (Movement movement in FindObjectsOfType<Movement>())
                {
                    movement.reversecontrols = true;
                    StartCoroutine(Resett(movement));
                }
                melonTimer timerAnimation = FindObjectOfType<melonTimer>();
                timerAnimation.MelonTimer();
                break;
        }
    }

    IEnumerator Resett(Movement movement)
    {
        yield return new WaitForSeconds(duration);
        movement.reversecontrols = false;
        Destroy(gameObject);
    }
}
