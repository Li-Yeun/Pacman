using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletBehaviour : MonoBehaviour {

    public bool pelleteaten;
    public GameObject pellet;
    public ScoreCounter pelletscore;

	// Use this for initialization
	void Start () {
        pelleteaten = false;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EatPellet();
        }
    }

    public bool EatPellet()
    {
        return pelleteaten = true;
    }

    // Update is called once per frame
    void Update () {
		if (pelleteaten)
        {
            pellet.SetActive(false);
            pelletscore.PelletPoints();
        }
	}
}
