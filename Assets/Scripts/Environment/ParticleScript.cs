using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour {

    ParticleSystem particleSystem;
	void Start () {
        particleSystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        Invoke("TurnOffSmokes", 10f);
    }

    void TurnOffSmokes()
    {
        particleSystem.loop = false;
        Invoke("DestroySmoke", 11);
    }

    void DestroySmoke()
    {
        Destroy(gameObject);
    }
}
