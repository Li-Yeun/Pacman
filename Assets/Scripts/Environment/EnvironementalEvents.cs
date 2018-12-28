using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnvironementalEvents : NetworkBehaviour {

    [SerializeField] GameObject Smoke, FireWorks, SandStorm, Water, Confusion;
    [SerializeField] Transform parent;

	// Update is called once per frame
	void Update () {
        if (!hasAuthority)
            return;
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
        else if (Input.GetKeyDown("6"))
        {
            ActivateEvent(Confusion);
        }
    }

    private void ActivateEvent(GameObject gameObject)
    {
        if (FindObjectsOfType<ParticleScript>().Length == 0)
        {
            GameObject go = Instantiate(gameObject);
            go.transform.parent = parent;
            NetworkServer.Spawn(go);
        }
    }
}
