using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnWater : NetworkBehaviour
{

    [SerializeField] GameObject WaterFirstPerson, WaterThirdPerson, Waterfall;      // De references van de water prefabs die gespawned moeten worden
    private GameObject Pacman;
    private GameObject[] Ghosts;
    private float PacmanSpeed, GhostSpeed;                                          // De standaard snelheden van Pacman en de Ghosts

    void Start()
    {

        Camera[] camera = FindObjectsOfType<Camera>();
        foreach (Camera cam in camera)
        {
            if (cam == null)
                return;

            else if (cam.name == "Top Down Camera(Clone)")
                cam.orthographic = false;                                           // De camera projectie veranderen van 2D naar 3D
        }
        Pacman = GameObject.FindGameObjectWithTag("Player");
        Ghosts = GameObject.FindGameObjectsWithTag("Enemy");
        PacmanSpeed = Pacman.GetComponent<PacmanMovement>().Speed.x;
        if (Ghosts.Length != 0)
        {
            GhostSpeed = Ghosts[0].GetComponent<Movement>().speed;
        }
        SpawnPrefab(WaterFirstPerson, "SlowDownMovement", 9f);                      // Movement van ghost en pacman worden geslowed na 9 seconde
        SpawnPrefab(WaterThirdPerson, "ResetMovement", 37f);                        // Movement van ghost en pacman worden na de oude waarde terug gezet na 9=37 seconde        
        Invoke("ResetWater", 34f);                                                  // Het water laten zakken na 34 seconde
        Instantiate(Waterfall, GameObject.Find("EveryObject").transform);           // De water prefab wordt hier gecreërd

    }

    private void ResetWater()
    {
        RaisingWater[] allWater = FindObjectsOfType<RaisingWater>();
        {
            foreach (RaisingWater water in allWater)
            {
                water.Lock = true;      // Deze lockt zorgt ervoor dat de water gaat zakken als die true is
            }
        }
    }
    private void SpawnPrefab(GameObject Event, string Method, float time)
    {
        GameObject gameObject = Instantiate(Event, GameObject.Find("EveryObject").transform);
        Invoke(Method, time);
        Destroy(gameObject, 40f);   // Het water wordt hiet na 40 seconde vernietigt
    }
    private void SlowDownMovement()
    {
        SetMovementSpeed(1.2f, 1);
    }

    private void SetMovementSpeed(float PacmanMovementSpeed, float GhostMovementSpeed)
    {
        Pacman.GetComponent<PacmanMovement>().Speed.x = PacmanMovementSpeed;                    // De snelheid van pacman op de x-as wordt verandert
        Pacman.GetComponent<PacmanMovement>().Speed.z = PacmanMovementSpeed;                    // De snelheid van pacman op de z-as wordt verandert
        foreach (GameObject Ghost in Ghosts)
        {
            if (Ghost == null)
                return;
            Ghost.GetComponent<Movement>().speed = GhostMovementSpeed;                          // De snelheid van de geestjes wordt verandert
        }
    }

    private void ResetMovement()
    {
        Camera[] camera = FindObjectsOfType<Camera>();
        foreach (Camera cam in camera)
        {
            if (cam == null)
                return;
            else if (cam.name == "Top Down Camera(Clone)")
                cam.orthographic = true;                                                         // De camera projectie veranderen van 3D naar 2D
        }
        FindObjectOfType<EnvironementalEvents>().ResetTimer();
        SetMovementSpeed(PacmanSpeed, GhostSpeed);
    }

    public void Reset()
    {
        Destroy(gameObject);
    }
}