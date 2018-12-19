using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletBehaviour : MonoBehaviour {

    bool pelleteaten;
    private ScoreCounter pelletscore;
    public float Timer = 0;


    // Use this for initialization
    void Start ()
    {
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
            Destroy(gameObject);
            pelletscore.PelletPoints();
        }
	}
}
