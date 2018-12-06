using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironementalEvents : MonoBehaviour {

    [SerializeField] GameObject Smoke;
    [SerializeField] ParticleSystem Smokes;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("2") && Smoke.activeSelf == false)
        {
            Smoke.SetActive(true);
            Invoke("TurnOffSmokes", 5f);
        }
	}

    void TurnOffSmokes()
    {
        Smokes.loop = false;
        Invoke("DeActivateSmoke", 11);
    }

    void DeActivateSmoke()
    {
        Smokes.loop = true;
        Smoke.SetActive(false);
    }

    

}
