﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    public Text scoreCounter;
    public int counter = 0;

	// Use this for initialization
	void Start () {
        scoreCounter.text = "SCORE: " + counter;
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

	// Update is called once per frame
	void Update () {
        scoreCounter.text = "SCORE: " + counter;
	}
}