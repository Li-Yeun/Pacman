using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnvironementalEvents : NetworkBehaviour {

    [SerializeField] GameObject Smoke, FireWorks, SandStorm, Water, Confusion;
    [SerializeField] float EventCooldown;
    private List<GameObject> Events;
    private float timer = 0f;
    private bool Lock = false;

    void Start()
    {
        Events = new List<GameObject> { Smoke, FireWorks, SandStorm, Water, Confusion };
    }

    void Update()
    {
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

    private void Spawn(GameObject gameObject)
    {
        GameObject go = Instantiate(gameObject); //TODO parent toevoegen
        NetworkServer.Spawn(go);
    }

    public void ResetTimer()
    {
        timer = 0f;
        Lock = false;
    }
}
