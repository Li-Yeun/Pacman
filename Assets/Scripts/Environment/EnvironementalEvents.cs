using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironementalEvents : MonoBehaviour {

    [SerializeField] GameObject Smoke, FireWorks, SandStorm, Water;
    [SerializeField] Transform parent;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("2"))
        {
            ActivateEvent(Smoke);
        }
        else if (Input.GetKeyDown("4"))
        {
            ActivateEvent(FireWorks);
        }
        else if (Input.GetKeyDown("5"))
        {
            ActivateEvent(SandStorm);
        }
    }

    private void ActivateEvent(GameObject gameObject)
    {
        if (FindObjectsOfType<SmokeScript>().Length == 0)
        {
            GameObject go = Instantiate(gameObject);
            go.transform.parent = parent;
        }
    }
}
