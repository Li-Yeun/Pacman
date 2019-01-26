using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnvironementalEvents : NetworkBehaviour {

    [SerializeField] GameObject Smoke, FireWorks, SandStorm, Water, Confusion;  // alle events die gespawned kunnen worden
    [SerializeField] float EventCooldown;                                       // de cooldown's tijd voor het spawnen van het volgende event
    private List<GameObject> Events;                                            // de lijst waarin alle event zijn opgeslagen
    private float timer = 0f;                                                   // de timer die de tijd bijhoudt voor het volgende event
    private bool Lock = false;                                                  // de Lock die ervoor zorgt dat er maar 1 event gespawned na iedere cooldown


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
            int Event = Random.Range(0, Events.Count);  // een random event kiezen uit de Events lijst
            Spawn(Events[Event]);                       // de random event laten spawnen
            Events.Remove(Events[Event]);               // de random event uit de Events lijst wissen zodat deze niet meerdere keren gekozen kan worden
            if (Events.Count <= 0)                      // als de Events lijst geen event meer bevat, dan worden deze lijst weer opnieuw bijgevuld naar de oude staat
            {
                Events = new List<GameObject> { Smoke, FireWorks, SandStorm, Water, Confusion };
            }
        }
    }

    // Methode om de events naar alle clients te laten spawnen
    private void Spawn(GameObject gameObject)
    {
        GameObject go = Instantiate(gameObject, GameObject.Find("EveryObject").transform); //TODO parent toevoegen
        NetworkServer.Spawn(go);
    }

    // De Lock en de timer resetten naar zodat de volgende event gespawned kan worden naar x secondes
    public void ResetTimer()
    {
        timer = 0f;
        Lock = false;
    }

    public void Reset()
    {
        ResetTimer();
    }
}
