using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Laat de powerpil verdwijnen als pacman deze eet
/// Triggerd ook het event dat pacman de spookjes kan gaan eten ipv andersom.
/// </summary>
public class PowerpilBehaviour : MonoBehaviour {

    public bool powerpileaten;
    public GameObject powerpil;
    public PacmanAttacking AnimationScriptSpookjes;
    public PacmanAttacking view3d;
	// Use this for initialization
	void Start ()
    {
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
            powerpil.SetActive(false);
            AnimationScriptSpookjes.PacmanIsTheHunter();
            view3d.PacmanIsTheHunter();
        }   
	}
}
