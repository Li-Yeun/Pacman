using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Laat de powerpil verdwijnen als pacman deze eet
/// Triggerd ook het event dat pacman de spookjes kan gaan eten ipv andersom.
/// </summary>
public class PowerpilBehaviour : MonoBehaviour {

    public bool powerpileaten;
    private PacmanAttacking[] AnimationScriptSpookjes;
    private ScoreCounter powerpilscore;

    // Use this for initialization
    void Start ()
    {
        powerpilscore = FindObjectOfType<ScoreCounter>();
        AnimationScriptSpookjes = FindObjectsOfType<PacmanAttacking>();
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

            foreach (PacmanAttacking Ghost in AnimationScriptSpookjes)
                Ghost.PacmanIsTheHunter();

        }   
	}
}
