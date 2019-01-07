using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reversecontrolsPowerup : MonoBehaviour {

    public GameObject Melon;
    private PacmanMovement pacmanMovement;

    void Start ()
    {
        Melon = this.gameObject;
        
    }

    public void PacmanInstantiated()
    {
        pacmanMovement = FindObjectOfType<PacmanMovement>();
    }

void OnTriggerStay(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Pellet":
                Destroy(Melon);
                break;
            case "Player":
                Melon.SetActive(false);
                pacmanMovement.Reversecontrols = true;
                break;
        }
    }

    void Update () {
		
	}
}
