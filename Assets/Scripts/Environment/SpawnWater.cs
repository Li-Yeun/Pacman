using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnWater : NetworkBehaviour {

    [SerializeField] GameObject WaterFirstPerson, WaterThirdPerson, Waterfall;      // Spawnable Prefabs
    [SerializeField] GameObject Pacman;
    [SerializeField] GameObject[] Ghosts;
    private float PacmanSpeed, GhostSpeed;   // The defaulth speed of Pacman && The defaulth speed of the Ghosts

    void Start () {

        Camera[] camera = FindObjectsOfType<Camera>();
        foreach (Camera cam in camera)
        {
            if (cam == null)
                return;

            else if (cam.name == "Top Down Camera(Clone)")
                cam.orthographic = false;
        }
        Pacman = GameObject.FindGameObjectWithTag("Player");
        Ghosts = GameObject.FindGameObjectsWithTag("Enemy");
        PacmanSpeed = Pacman.GetComponent<PacmanMovement>().Speed.x;
        if (Ghosts.Length != 0)
        {
            GhostSpeed = Ghosts[0].GetComponent<Movement>().speed;
        }
        SpawnPrefab(WaterFirstPerson, "SlowDownMovement", 9f);
        SpawnPrefab(WaterThirdPerson, "ResetMovement", 37f);
        Invoke("ResetWater", 34f);
        Instantiate(Waterfall);

    }

    private void ResetWater()
    {
        RaisingWater[] allWater = FindObjectsOfType<RaisingWater>();
        {
            foreach (RaisingWater water in allWater)
            {
                water.Lock = true;
            }
        }
    }
    private void SpawnPrefab(GameObject Event, string Method, float time)
    {
        GameObject gameObject = Instantiate(Event);
        Invoke(Method, time);
        Destroy(gameObject, 40f);
    }
    private void SlowDownMovement()
    {
        SetMovementSpeed(1.2f,1);
    }

    private void SetMovementSpeed(float PacmanMovementSpeed, float GhostMovementSpeed)
    {
        Pacman.GetComponent<PacmanMovement>().Speed.x = PacmanMovementSpeed;
        Pacman.GetComponent<PacmanMovement>().Speed.z = PacmanMovementSpeed;
        foreach (GameObject Ghost in Ghosts)
        {
            if (Ghost == null)
                return;
            Ghost.GetComponent<Movement>().speed = GhostMovementSpeed;
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
                cam.orthographic = true;
        }
        FindObjectOfType<EnvironementalEvents>().ResetTimer();
        SetMovementSpeed(PacmanSpeed, GhostSpeed);
    }

    public void Reset()
    {
        Camera[] camera = FindObjectsOfType<Camera>();
        foreach (Camera cam in camera)
        {
            if (cam == null)
                return;
            else if (cam.name == "Top Down Camera(Clone)")
                cam.orthographic = true;
        }
        FindObjectOfType<EnvironementalEvents>().ResetTimer();
        SetMovementSpeed(PacmanSpeed, GhostSpeed);
        Destroy(gameObject);
    }
}
