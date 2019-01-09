using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnWater : NetworkBehaviour {

    [SerializeField] GameObject WaterFirstPerson,WaterThirdPerson,Waterfall;
    [SerializeField] GameObject Pacman;
    [SerializeField] GameObject[] Ghosts;
    float PacmanSpeed;
    int GhostSpeed;
    // Use this for initialization
    void Start () {
        Pacman = GameObject.FindGameObjectWithTag("Player");
        Ghosts = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject water1 = Instantiate(WaterFirstPerson);
        GameObject water2 = Instantiate(WaterThirdPerson);
        Instantiate(Waterfall);
        PacmanSpeed = Pacman.GetComponent<PacmanMovement>().Speed.x;
        if (Ghosts != null)
        {
            GhostSpeed = Ghosts[0].GetComponent<Movement>().speed;
        }

        Invoke("SlowDownMovement", 10f);
        Destroy(water1,40f);
        Destroy(water2,40f);
        Invoke("ResetMovement", 39f);

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

    public void ResetMovement()
    {
        Pacman.GetComponent<PacmanMovement>().Speed.x = PacmanSpeed;
        Pacman.GetComponent<PacmanMovement>().Speed.z = PacmanSpeed;
        foreach (GameObject Ghost in Ghosts)
        {
            if (Ghost == null)
                return;
            Ghost.GetComponent<Movement>().speed = GhostSpeed;
        }
    }

}
