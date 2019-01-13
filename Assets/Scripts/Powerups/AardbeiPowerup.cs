using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AardbeiPowerup : MonoBehaviour {

    public float duration = 15f;

    void OnTriggerStay(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Pellet":
                Destroy(col.gameObject);
                break;
            case "Player":
                gameObject.GetComponent<SphereCollider>().enabled = false;
                gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                PacmanMovement pacmanMovement = col.GetComponent<PacmanMovement>();
                aardbeiTimer timerAnimation = FindObjectOfType<aardbeiTimer>();
                timerAnimation.AardbeiTimer();
                StartCoroutine(Resett(pacmanMovement));
                break;
        }
        
    }
    private IEnumerator Resett(PacmanMovement pacmanMovement)
    {
        pacmanMovement.Speed.x++;
        pacmanMovement.Speed.z++;
        yield return new WaitForSeconds(duration);
        pacmanMovement.Speed.x--;
        pacmanMovement.Speed.z--;
        Destroy(gameObject);
    }
}
