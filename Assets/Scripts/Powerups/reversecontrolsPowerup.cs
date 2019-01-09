using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This scripts reverses the movement controls of pacman when the melon is eaten.
/// </summary>
public class reversecontrolsPowerup : MonoBehaviour
{
    private PacmanMovement pacmanMovement;

    private void Start()
    {
        PacmanInstantiated();
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
                Destroy(col);
                break;
            case "Player":
                gameObject.SetActive(false); ///transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 200, gameObject.transform.position.z);
                pacmanMovement.Reversecontrols = true;
                Invoke("Resett", 15f);
                break;
        }
    }
    private void Resett()
    {
        pacmanMovement.Reversecontrols = false;
        Destroy(gameObject);
    }
}
