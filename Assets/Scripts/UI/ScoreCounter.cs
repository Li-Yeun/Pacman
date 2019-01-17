using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    private Text scoreCounter;
    int counter = 0;

	// Use this for initialization
	void Start () {
        scoreCounter = GetComponent<Text>();
        scoreCounter.text = "" + counter;
	}
	
    // Adds points to the scorecounter when a powerpil is eaten
    public void PowerpilPoints()
    {
        counter += 50;
    }

    // Adds points to the scorecounter when a pellet is eaten
    public void PelletPoints()
    {
        counter += 10;
    }

    // Adds points to the scorecounter when a fruit is eaten
    public void FruitPoints()
    {
        counter += 100;
    }

	// Update is called once per frame
	void Update () {
        scoreCounter.text = "" + counter;

        if (FindObjectsOfType<PelletBehaviour>().Length == 0 && FindObjectsOfType<PowerpilBehaviour>().Length == 0)
        {
            Debug.Log("PACMAN WINS!");
        }
	}

    public void Reset()
    {
        counter = 0;
    }
}
