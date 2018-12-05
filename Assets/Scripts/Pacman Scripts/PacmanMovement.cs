using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour {

    [Header("Directional Speed")]
    [SerializeField] float xspd,yspd, zspd;

    [Header("Camera's")]
    [SerializeField] GameObject NormalCamera, ThirdPerson, MiniMap, EnemyCamera;

    [Header("Spawner")]
    [SerializeField] GameObject Spawner;

    string[] Directions;
    public string currentDirection, p_Direction;
    Vector3[] RotateList = { new Vector3(0, 90, 0), new Vector3(0, 180, 0), new Vector3(0, 270, 0), new Vector3(0, 0, 0)};
    Vector3 rotation;
    int directionIndex, rotateIndex;
    bool SwitchControls;
    public KeyCode currentKey, p_Key;
    Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Directions = new string[] { "Up","Right", "Down", "Left" };
        rotation = RotateList[rotateIndex];
        directionIndex = 0;
        currentDirection = Directions[directionIndex];
        SwitchControls = false;
    }

    // Update is called once per frame
    void Update()
    {
        p_Direction = currentDirection;
        p_Key = currentKey;
        KeyInput();

        if (transform.position.y < -20)
        {
            StartDeathSequence();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                StartDeathSequence();
                break;
            case "Friendly":
                break;
            default:
                rigidbody.useGravity = false;
                break;
        }

    }

    private void StartDeathSequence()
    {
        transform.position = Spawner.transform.position;
        rigidbody.useGravity = true;
        directionIndex = 0;
        currentDirection = Directions[directionIndex];
    }

    void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchControls = !SwitchControls;
        }
        if (SwitchControls)
        {
            NormalCamera.SetActive(false);
            ThirdPerson.SetActive(true);
            MiniMap.SetActive(true);
            ThirdPersonMode();
        }
        else
        {
            NormalCamera.SetActive(true);
            ThirdPerson.SetActive(false);
            MiniMap.SetActive(false);
            NormalMode();
        }
    }

    private void ThirdPersonMode()
    {
        /*   if (LockRotateMovement == true)
               return;
               */
        if (Input.GetKeyDown(KeyCode.S))
        {
            //LockRotateMovement = true;
            currentKey = KeyCode.S;
            AddDirectionIndex(2);
            Rotation();
            Move_Player();

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            //LockRotateMovement = true;
            currentKey = KeyCode.A;
            AddDirectionIndex(-1);
            Rotation();
            Move_Player();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            //LockRotateMovement = true;
            currentKey = KeyCode.D;
            AddDirectionIndex(1);
            Rotation();
            Move_Player();

        }

    }

    private void NormalMode()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentDirection = Directions[0];
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            currentDirection = Directions[2];
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            currentDirection = Directions[3];
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            currentDirection = Directions[1];
        }

        Rotation();
        Move_Player();
    }

    public void AddDirectionIndex(int number)
    {
        directionIndex += number;
        if (directionIndex > Directions.Length - 1)
        {
            directionIndex -= Directions.Length;
        }
        else if (directionIndex < 0)
        {
            directionIndex = 3;
        }
        currentDirection = Directions[directionIndex];
    }
    public void Move_Player()
    {
        if (currentDirection == "Up")
        {
            rigidbody.velocity = new Vector3(0, 0, zspd);
        }
        else if (currentDirection == "Right")
        {
            rigidbody.velocity = new Vector3(xspd, 0, 0);
        }
        else if (currentDirection == "Down")
        {
            rigidbody.velocity = new Vector3(0, 0, -zspd);
        }
        else if (currentDirection == "Left")
        {
            rigidbody.velocity = new Vector3(-xspd, 0, 0);
        }
    }

    private void Rotation()
    {
        if (currentDirection == "Up")
        {
            rigidbody.rotation = Quaternion.Euler(RotateList[0]);
        }
        else if (currentDirection == "Down")
        {
            rigidbody.rotation = Quaternion.Euler(RotateList[2]);
        }
        else if (currentDirection == "Left")
        {
            rigidbody.rotation = Quaternion.Euler(RotateList[3]);
        }
        else if (currentDirection == "Right")
        {
            rigidbody.rotation = Quaternion.Euler(RotateList[1]);
        }
    }

    public string CurrentDirection
        {
            get { return currentDirection; }
        }
    public string P_Direction
    {
        get { return p_Direction; }
    }
    public Vector3 PacmanPos
    {
        get { return transform.position; }
    }
}
