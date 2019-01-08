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
        else if (Input.GetKeyDown("7"))
        {
            ActivateEvent(Water);
        }
    }

    private void ActivateEvent(GameObject gameObject)
    {
        if (GameObject.FindGameObjectWithTag("Event") == null)
        {
            Spawn(gameObject);
        }
    }

    /*
    private void ActivateMultipleEvents(GameObject gameObject1, GameObject gameObject2, GameObject gameObject3)
    {
        if (GameObject.FindGameObjectWithTag("Event") == null)
        {
            Spawn(gameObject1);
            Spawn(gameObject2);
            Spawn(gameObject3);
        }
    }
    */

    private void Spawn(GameObject gameObject)
    {
        GameObject go = Instantiate(gameObject);
        go.transform.parent = parent;
        NetworkServer.Spawn(go);
    }

    public void Reset()
    {
        if(GameObject.FindGameObjectWithTag("Event") != null)
        {
            foreach(GameObject Event in GameObject.FindGameObjectsWithTag("Event"))
            {
                Destroy(Event);
            }
        }
    }
}
