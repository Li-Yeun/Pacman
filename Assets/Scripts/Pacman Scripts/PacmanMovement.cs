using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Smooth;

public class PacmanMovement : NetworkBehaviour {

    [SerializeField] KeyCode[] Controls;                // De Control voor pacman om te bewegen/springen/ability gebruiken

    [Header("Directional Speed")]
    [SerializeField] public Vector3 Speed;              // De snelheid van pacman waarmee hij beweegt

    [Header("Special Triggers")]
    [SerializeField] SpecialTrigger2 left, right, Movement;
    Vector3[] RotateList = { new Vector3(0, 90, 0), new Vector3(0, 180, 0), new Vector3(0, 270, 0), new Vector3(0, 0, 0) };  // De standaard rotaties die pacman moet gebruiken bij speciefieke methodes

    public bool AnimationLock = false;                  // Een lock voor  movement wanneer er een animatie wordt afgespeeld
    public int currentDirection;                        // De hudige directie waar pacman naar kijkt

    private bool Teleporterlock = false, LockMovement;  // Andere soorten Locks voor de movement van pacman 
    private KeyCode currentKey, p_Key;                  // Deze variable houdt bij welke knoppen er gedrukt waren
    private Vector3 defaulthSpeed;                      // De standaard snelheid van pacman
    private Rigidbody rb;
    private GameObject[] Ghosts, Spawners;                     
    private GameObject Spawner;                         // De spawner waar pacman gespawned moet worden

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
        defaulthSpeed = Speed;                          // Zet de defaulth waarde van de snelheid van pacman op de waarde waarop pacman gespawned is
        FindObjectOfType<General>().PacmanBroadcast();
        if(FindObjectsOfType<Movement>().Length == 4)
        {
            Invoke("ResetGameWithDelay", 3f);           // Reset de game als 4 ghost gejoined zijn na 3seconde
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


    // De methode voor wanneer pacman op een van de movement controls drukt
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

    // Methode voor wat er moet gebeuren als de movement controls gedrukt zijn
    private void HandleKeyInput(KeyCode KeyInput, int Direction)
    {
        if (KeyInput != Controls[1])            // Checken of de S knop niet ingedrukt is
        {
            LockMovement = true;                // Een Lock toepassen op de movement zodat A of D niet gespammed kan worden waardoor hij een rondje gaat draaien
            Invoke("UnlockMovement", 0.5f);     // De Lock verijderen na 1/2 seconde
        }
        currentDirection += Direction;          // De nieuwe directie waar pacman naartoe moet gaan
        if (currentDirection > 3)
        {
            currentDirection -= 4;
        }
        else if (currentDirection < 0)
        {
            currentDirection = 3;
        }
        currentKey = KeyInput;
        Rotation();                             // De rotatie van pacman goed zetten gebaseerd op de richting
    }

    // Deze methode verandert de velocity van pacman.
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

    // Deze methode verandert de roatie van pacman.
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

    // Deze methode wordt gebruikt wanneer Pacman de "Enemy" heeft geraakt.
    private void StartDeathSequence()
    {
        GameObject[] Ghosts = GameObject.FindGameObjectsWithTag("Enemy");       // Vindt alle ghost die in de scene gespawned zijn en zet ze in een lijst
        if (Ghosts.Length == 0)
        {
            // Als er geen Ghost is, spawn dan op een random locatie
            int random = Random.Range(0, Spawners.Length);
            Spawner = Spawners[random];
        }
        else // Deze methode calcuurt de spawn positie van pacman, zo dat pacman de meest gunstige spawn locatie heeft
        {
            float furthestDistanceGhost = 0;            
            GameObject furthestSpawner = null;
            foreach (GameObject spawner in Spawners)
            {
                float shortestDistanceOfGhost = 0;
                foreach (GameObject ghost in Ghosts)
                {

                    if (shortestDistanceOfGhost == 0)  //Deze conditie is bedoeld als dit het eerste geestje is die gecheked moet van de lijst
                    {  
                       shortestDistanceOfGhost = Mathf.Abs(spawner.transform.position.x - ghost.transform.position.x) + Mathf.Abs(spawner.transform.position.z - ghost.transform.position.z);
                    }
                    else if (Mathf.Abs(spawner.transform.position.x - ghost.transform.position.x) + Mathf.Abs(spawner.transform.position.z - ghost.transform.position.z) < shortestDistanceOfGhost)
                    { // Checken of dit dichterbijer is bij deze spawnlocatie, zowel maakt dit de nieuwe shortest distance
                        shortestDistanceOfGhost = Mathf.Abs(spawner.transform.position.x - ghost.transform.position.x) + Mathf.Abs(spawner.transform.position.z - ghost.transform.position.z);
                    }
                }

                if(shortestDistanceOfGhost > furthestDistanceGhost) //Als de shortest distance van dit geestje bij deze spawner groter is dan de algemene furthest distance voor alle spawner, zet deze spawner dan als verste spawner van de al gecheckte spawn locatie
                {
                    furthestDistanceGhost = shortestDistanceOfGhost;
                    furthestSpawner = spawner;
                }
            }
            Spawner = furthestSpawner; // Pacman spawner wordt hier zo gezet bij een spawnlocatie die het verst van de dichtsbijzijnde spookje is
        }

        GetComponent<SmoothSync>().teleportAnyObjectFromServer(Spawner.transform.position, gameObject.transform.rotation, gameObject.transform.localScale); // Pacman wordt hier smooth geteleporteerd naar zijn spawnlocatie
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
            Invoke("ResetGameWithDelay", 3f);
        }
    }

    private void ResetGameWithDelay()
    {
        FindObjectOfType<General>().ResetBroadCast();
    }

    public void Reset()
    {
        Speed = defaulthSpeed;
        StartDeathSequence();
        GetComponent<SmoothSync>().teleportAnyObjectFromServer(new Vector3(3f, 2f, -2f), gameObject.transform.rotation, gameObject.transform.localScale);
    }
}
