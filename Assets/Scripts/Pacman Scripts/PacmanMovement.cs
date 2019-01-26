using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Smooth;

public class PacmanMovement : NetworkBehaviour {

    [SerializeField] KeyCode[] Controls;

    [Header("Directional Speed")]
    [SerializeField] public Vector3 Speed;

    [Header("Special Triggers")]
    [SerializeField] SpecialTrigger2 left, right, Movement;
    Vector3[] RotateList = { new Vector3(0, 90, 0), new Vector3(0, 180, 0), new Vector3(0, 270, 0), new Vector3(0, 0, 0) };

    public bool AnimationLock = false;
    public int currentDirection;

    private bool Teleporterlock = false, LockMovement;
    private KeyCode currentKey, p_Key;
    private Vector3 defaulthSpeed;
    private Rigidbody rb;
    private GameObject[] Ghosts, Spawners;
    private GameObject Spawner;

    public Vector3 Position
    {
        get { return gameObject.transform.position; }
        set { gameObject.transform.position = value; }
    }

    void Start()
    {
        gameObject.transform.parent = GameObject.FindGameObjectWithTag("Object Parent").transform;
        Spawners = GameObject.FindGameObjectsWithTag("Pacman Respawn");

        transform.position = new Vector3(3f, 2f, -2f);
        rb = GetComponent<Rigidbody>();
        currentDirection = 0;
        LockMovement = false;
        defaulthSpeed = Speed;
        FindObjectOfType<General>().PacmanBroadcast();
        if(FindObjectsOfType<Movement>().Length == 4)
        {
            FindObjectOfType<General>().ResetBroadCast();
        }
    }

    void Update()
    {
        if (!hasAuthority)
            return;
        if (Movement.Collision) { Teleporterlock = true; } else { Teleporterlock = false; }
        p_Key = currentKey;
        if (!AnimationLock)
        {
            Move_Player();
            if (!Teleporterlock)
            {
               FirstPersonMode();
            }
        }
        else { rb.velocity = Vector3.zero; }
    }

    private void FirstPersonMode()
    {
      
           if (Input.GetKeyDown(Controls[1]))
            {
                HandleKeyInput(Controls[1], 2);
            }
            else if (Input.GetKey(Controls[0]) && !left.Collision && !LockMovement)
            {
                HandleKeyInput(Controls[0], -1);
            }
            else if (Input.GetKey(Controls[2]) && !right.Collision && !LockMovement)
            {
                HandleKeyInput(Controls[2], 1);
            }
       
    }
    
    private void HandleKeyInput(KeyCode KeyInput, int Direction)
    {
        if (KeyInput != Controls[1])
        {
            LockMovement = true;
            Invoke("UnlockMovement", 0.5f);
        }
        currentDirection += Direction;
        if (currentDirection > 3)
        {
            currentDirection -= 4;
        }
        else if (currentDirection < 0)
        {
            currentDirection = 3;
        }
        currentKey = KeyInput;
        Rotation();
    }

    private void Move_Player()
    {
        if (currentDirection == 0)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, Speed.z);
        }
        else if (currentDirection == 1)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(Speed.x, 0, 0);
        }
        else if (currentDirection == 2)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -Speed.z);
        }
        else if (currentDirection == 3)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(-Speed.x, 0, 0);
        }
    }

    private void Rotation()
    {
        switch (currentDirection)
        {
            case 0: rb.rotation = Quaternion.Euler(RotateList[0]);
                     break;
            case 1: rb.rotation = Quaternion.Euler(RotateList[1]);
                     break;
            case 2: rb.rotation = Quaternion.Euler(RotateList[2]);
                     break;
            case 3: rb.rotation = Quaternion.Euler(RotateList[3]);
                     break;
        }
    }

    private void StartDeathSequence() // Gebruik deze methode wanneer Pacman de "Enemy" heeft geraakt.
    {
        GameObject[] Ghosts = GameObject.FindGameObjectsWithTag("Enemy");
        if (Ghosts.Length == 0)
        {
            int random = Random.Range(0, Spawners.Length);
            Spawner = Spawners[random];
        }
        else
        {
            float furthestDistanceGhost = 0;
            GameObject furthestSpawner = null;
            foreach (GameObject spawner in Spawners)
            {
                float shortestDistanceOfGhost = 0;
                foreach (GameObject ghost in Ghosts)
                {

                    if (shortestDistanceOfGhost == 0)
                    {
                       shortestDistanceOfGhost = Mathf.Abs(spawner.transform.position.x - ghost.transform.position.x) + Mathf.Abs(spawner.transform.position.z - ghost.transform.position.z);
                    }
                    else if (Mathf.Abs(spawner.transform.position.x - ghost.transform.position.x) + Mathf.Abs(spawner.transform.position.z - ghost.transform.position.z) < shortestDistanceOfGhost)
                    {
                        shortestDistanceOfGhost = Mathf.Abs(spawner.transform.position.x - ghost.transform.position.x) + Mathf.Abs(spawner.transform.position.z - ghost.transform.position.z);
                    }
                }

                if(shortestDistanceOfGhost > furthestDistanceGhost)
                {
                    furthestDistanceGhost = shortestDistanceOfGhost;
                    furthestSpawner = spawner;
                }
            }
            Spawner = furthestSpawner;
        }

        GetComponent<SmoothSync>().teleportAnyObjectFromServer(Spawner.transform.position, gameObject.transform.rotation, gameObject.transform.localScale);
        GetComponent<AnimatorScript>().EndAnimation();
        GetComponent<Rigidbody>().useGravity = true;
        Invoke("DisableGravity", 1f);
    }

    private void UnlockMovement()
    {
        LockMovement = false;
    }

    private void DisableGravity()
    {
        GetComponent<Rigidbody>().useGravity = false;
    }

    public void GhostInstantiated()
    {
        if (FindObjectsOfType<Movement>().Length == 4)
        {
            FindObjectOfType<General>().ResetBroadCast();
        }
    }

    public void Reset()
    {
        Speed = defaulthSpeed;
        StartDeathSequence();
        GetComponent<SmoothSync>().teleportAnyObjectFromServer(new Vector3(3f, 2f, -2f), gameObject.transform.rotation, gameObject.transform.localScale);
    }
}
