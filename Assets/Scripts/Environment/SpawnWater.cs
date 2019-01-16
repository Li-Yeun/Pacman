﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnWater : NetworkBehaviour {

    [SerializeField] GameObject WaterFirstPerson,WaterThirdPerson,Waterfall;      // Spawnable Prefabs
    [SerializeField] GameObject Pacman;
    [SerializeField] GameObject[] Ghosts;
    float PacmanSpeed;   // The defaulth speed of Pacman
    int GhostSpeed;      // The defaulth speed of the Ghosts

    void Start () {

        Camera[] camera = FindObjectsOfType<Camera>();
        foreach (Camera cam in camera)
        {
            if (cam == null)
                return;
            else if (cam.name == "Top Down Camera(Clone)")
                cam.orthographic = !cam.orthographic;
        }
        Pacman = GameObject.FindGameObjectWithTag("Player");
        Ghosts = GameObject.FindGameObjectsWithTag("Enemy");
        PacmanSpeed = Pacman.GetComponent<PacmanMovement>().Speed.x;
        if (Ghosts.Length != 0)
        {
            GhostSpeed = Ghosts[0].GetComponent<Movement>().speed;
        }
        SpawnPrefab(WaterFirstPerson, "SlowDownMovement", 10f);
        SpawnPrefab(WaterThirdPerson, "ResetMovement", 40f);
        Instantiate(Waterfall);

    }

    public void SpawnPrefab(GameObject Event, string Method, float time)
    {
        GameObject gameObject = Instantiate(Event);
        Invoke(Method, time);
        Destroy(gameObject, 40f);
    }
    public void SlowDownMovement()
    {
        SetMovementSpeed(0.5f,1);
    }

    private void SetMovementSpeed(float PacmanMovementSpeed, int GhostMovementSpeed)
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

    public void ResetMovement()
    {
        Camera[] camera = FindObjectsOfType<Camera>();
        foreach (Camera cam in camera)
        {
            if (cam == null)
                return;
            else if (cam.name == "Top Down Camera(Clone)")
                cam.orthographic = !cam.orthographic;
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
