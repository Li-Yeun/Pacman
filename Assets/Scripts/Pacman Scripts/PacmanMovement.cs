using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PacmanMovement : NetworkBehaviour {

    [SerializeField] KeyCode[] Controls;

    [Header("Directional Speed")]
    [SerializeField] public Vector3 Speed;

    [Header("Spawner")]
    GameObject Spawner;

    [Header("Special Triggers")]
    [SerializeField] SpecialTrigger2 left, right, Movement;
    Vector3[] RotateList = { new Vector3(0, 90, 0), new Vector3(0, 180, 0), new Vector3(0, 270, 0), new Vector3(0, 0, 0) };

    public Rigidbody rb;
    public bool AnimationLock = false;
    public bool Teleporterlock = false;
    public int currentDirection, p_Direction;
    public bool LockMovement;
    public KeyCode currentKey, p_Key;



    public Vector3 Position { get { return gameObject.transform.position; }
        set { gameObject.transform.position = value; }
    }



    void Start()
    {
        gameObject.transform.parent = GameObject.FindGameObjectWithTag("Object Parent").transform;
        Spawner = GameObject.Find("Pacman Spawn");
        transform.position = Spawner.transform.position;
        rb = GetComponent<Rigidbody>();
        currentDirection = 0;
        LockMovement = false;
    }

    void Update()
    {
        if (!hasAuthority)
            return;

        if (Movement.Collision) { Teleporterlock = true; } else { Teleporterlock = false; }
        p_Direction = currentDirection;
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

    public void Move_Player()
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

    public void StartDeathSequence() // Gebruik deze methode wanneer Pacman de "Enemy" heeft geraakt.
    {
        transform.position = Spawner.transform.position;
        GetComponent<Rigidbody>().useGravity = true;
        Invoke("DisableGravity", 1f);
    }

    private void UnlockMovement()
    {
        LockMovement = false;
    }

    public void DisableGravity()
    {
        GetComponent<Rigidbody>().useGravity = false;
        
    }

    public void Reset()
    {
        StartDeathSequence();
    }
}
