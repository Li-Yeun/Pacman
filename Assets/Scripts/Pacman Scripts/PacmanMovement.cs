using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour {

    [Header("Directional Speed")]
    [SerializeField] Vector3 Speed;

    [Header("Spawner")]
    [SerializeField] GameObject Spawner;

    [Header("Triggers")]
    [SerializeField] Trigger left, right;

    Vector3[] RotateList = { new Vector3(0, 90, 0), new Vector3(0, 180, 0), new Vector3(0, 270, 0), new Vector3(0, 0, 0) };

    public Rigidbody rb;
    public bool SwitchControls = false;
    public int currentDirection, p_Direction;
    bool LockMovement;
    public KeyCode currentKey, p_Key;

    void Start()
    {
        transform.position = Spawner.transform.position;
        rb = GetComponent<Rigidbody>();
        currentDirection = 0;
        LockMovement = false;
    }

    void Update()
    {
        p_Direction = currentDirection;
        p_Key = currentKey;
        if (SwitchControls) { TopDownMode(); }
        else { FirstPersonMode(); }
        Move_Player();
    }

    private void FirstPersonMode()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            HandleKeyInput(KeyCode.S, 2);
        }
        else if (Input.GetKey(KeyCode.A) && left.Collision == false && LockMovement == false)
        {
            HandleKeyInput(KeyCode.A, -1);
        }
        else if (Input.GetKey(KeyCode.D) && right.Collision == false && LockMovement == false)
        {
            HandleKeyInput(KeyCode.D, 1);
        } 
    }
    
    private void TopDownMode()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            HandleKeyInputNormal(0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            HandleKeyInputNormal(2);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            HandleKeyInputNormal(3);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            HandleKeyInputNormal(1);
        }
        Rotation();
    }

    private void HandleKeyInput(KeyCode KeyInput, int Direction)
    {
        if (KeyInput != KeyCode.S)
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

    public void HandleKeyInputNormal(int direction)
    {
        currentDirection = direction;
        Move_Player();
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
        if (currentDirection == 0)
        {
            rb.rotation = Quaternion.Euler(RotateList[0]);
        }
        else if (currentDirection == 2)
        {
            rb.rotation = Quaternion.Euler(RotateList[2]);
        }
        else if (currentDirection == 3)
        {
            rb.rotation = Quaternion.Euler(RotateList[3]);
        }
        else if (currentDirection == 1)
        {
            rb.rotation = Quaternion.Euler(RotateList[1]);
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
}
