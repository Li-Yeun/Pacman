using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnvironementalEvents : NetworkBehaviour {

    [SerializeField] GameObject Smoke, FireWorks, SandStorm, Water, Confusion;
    [SerializeField] Transform parent;
    [SerializeField] float EventCooldown;
    private List<GameObject> Events;
    float timer = 0f;
    bool Lock = false;

    private void Start()
    {
        Events = new List<GameObject> { Smoke, FireWorks, SandStorm, Water, Confusion };
    }

    void Update() {
        if (!hasAuthority)
            return;

        timer += Time.deltaTime;
        if (timer >= EventCooldown && Lock == false)
        {
            Lock = true;
            int Event = Random.Range(0, Events.Count);
            ActivateEvent(Events[Event]);
            Events.Remove(Events[Event]);
            if (Events.Count <= 0)
            {
                Events = new List<GameObject> { Smoke, FireWorks, SandStorm, Water, Confusion };
            }
        }

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

    public void ResetTimer()
    {
        timer = 0f;
        Lock = false;
    }
}
