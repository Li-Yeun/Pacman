using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Laat de powerpil verdwijnen als pacman deze eet
/// Triggerd ook het event dat pacman de spookjes kan gaan eten ipv andersom.
/// </summary>
public class PowerpilBehaviour : MonoBehaviour {

    [SerializeField] bool powerpileaten;
    private GhostStates[] AnimationScriptSpookjes;
    private ScoreCounter powerpilscore;

    // Use this for initialization
    void Start ()
    {
        powerpilscore = FindObjectOfType<ScoreCounter>();
        powerpileaten = false;
	}

    //Checks if pacman hit the trigger and if this is true removes the powerpil
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EatPowerpil();
        }
    }

    //Confirms powerpil has been eaten
    public bool EatPowerpil()
    {
        return powerpileaten = true;
    }

    // Update is called once per frame
    void Update () {
		if (powerpileaten)
        {
            gameObject.SetActive(false);
            powerpilscore.PowerpilPoints();
            foreach (GhostStates Ghost in AnimationScriptSpookjes)
            {
                if (Ghost == null)
                    Debug.Log("No Ghosts");
                Ghost.Vulnerable();
            }
        }   
	}
    public void GhostInstantiated()
    {
        AnimationScriptSpookjes = FindObjectsOfType<GhostStates>();
    }

}
