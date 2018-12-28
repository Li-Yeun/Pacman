using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnvironementalEvents : NetworkBehaviour {

    [SerializeField] GameObject Smoke, FireWorks, SandStorm, Water;
    [SerializeField] Transform parent;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("2"))
        {
            CmdActivateEvent(Smoke);
        }
        else if (Input.GetKeyDown("4"))
        {
            CmdActivateEvent(FireWorks);
        }
        else if (Input.GetKeyDown("5"))
        {
            CmdActivateEvent(SandStorm);
        }
    }

    [Command]
    private void CmdActivateEvent(GameObject gameObject)
    {
        if (FindObjectsOfType<ParticleScript>().Length == 0)
        {
            GameObject go = Instantiate(gameObject);
            go.transform.parent = parent;
            NetworkServer.Spawn(go);
        }
    }
}
