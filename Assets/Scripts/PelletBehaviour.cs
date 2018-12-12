using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletBehaviour : MonoBehaviour {

    bool pelleteaten;
    private ScoreCounter pelletscore;

	// Use this for initialization
	void Start () {

        pelletscore = FindObjectOfType<ScoreCounter>();
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
            gameObject.SetActive(false);  // Of Destroy(gameObject);    <--- Om de pellets echt uit de game te wissen.
            pelletscore.PelletPoints();
        }
	}
}
