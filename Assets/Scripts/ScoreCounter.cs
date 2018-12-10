using System.Collections;
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
	
    public void PowerpilPoints()
    {
        counter += 50;
    }

	// Update is called once per frame
	void Update () {
        scoreCounter.text = "SCORE: " + counter;
	}
}
