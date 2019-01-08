using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnWater : NetworkBehaviour {

    [SerializeField] GameObject WaterFirstPerson,WaterThirdPerson,Waterfall;
    [SerializeField] GameObject Pacman;
    [SerializeField] GameObject[] Ghosts;
    // Use this for initialization
    void Start () {
        Pacman = GameObject.FindGameObjectWithTag("Player");
        Ghosts = GameObject.FindGameObjectsWithTag("Enemy");
        Instantiate(WaterFirstPerson);
        Instantiate(WaterThirdPerson);
        Instantiate(Waterfall);
        Invoke("SlowDownMovement", 10f);
    }

    public void SlowDownMovement()
    {
        Pacman.GetComponent<PacmanMovement>().Speed.x = 0.5f;
        Pacman.GetComponent<PacmanMovement>().Speed.z = 0.5f;
        foreach(GameObject Ghost in Ghosts)
        {
            if (Ghost == null)
                return;
            Ghost.GetComponent<Movement>().speed = 1;
        }
    }
}
