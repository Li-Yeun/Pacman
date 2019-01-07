using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reversecontrolsPowerup : MonoBehaviour {

    public GameObject Melon;
    private PacmanMovement pacmanMovement;
    float timer = 0;
    float seconds = 15;

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
        //checks if collides with player or pellet, if collides with player then reverse the controls of the player
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

    private void Update()
    {
        //checkt if controls power up is active 
        if (pacmanMovement.Reversecontrols == true )        
        {
            timer += Time.deltaTime;

            //remove the powerup effect and reset the timer
            if (timer > seconds)
            {
                pacmanMovement.Reversecontrols = false;     
                timer = 0;
            }
        }
    }
}
