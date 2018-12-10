using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour {

    [Header("Directional Speed")]
    [SerializeField] float xspd = 2,yspd = 0, zspd = 2;  // Stel de horizontale snelheid van Pacman in de Editor vast.

    [Header("Camera's")]
    [SerializeField] GameObject NormalCamera, FirstPerson, MiniMap;  // Dit zijn de camera's die Pacman gebruikt.

    [Header("Spawner")]
    [SerializeField] GameObject Spawner; // Hier respawned Pacman als die dood gaat

    [Header("Triggers")]
    [SerializeField] Trigger left, right; 

    string[] Directions;
    int directionIndex;
    bool SwitchControls, LockMovement;
    private string currentDirection, p_Direction;
    private KeyCode currentKey, p_Key;

    Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Directions = new string[] { "Up","Right", "Down", "Left" };
        directionIndex = 0;
        currentDirection = Directions[directionIndex];
        SwitchControls = false;
        LockMovement = false;
    }

    // Update is called once per frame
    void Update()
    {
        p_Direction = currentDirection;
        p_Key = currentKey;
        NormalOrFirstPersonMode();
    }

    void NormalOrFirstPersonMode() // Deze methode switch tussen normal- en firstperson mode
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchControls = !SwitchControls;
        }

        if (SwitchControls)
        {
            NormalCamera.SetActive(false);
            FirstPerson.SetActive(true);
            MiniMap.SetActive(true);
            FirstPersonMode();
        }
        else
        {
            NormalCamera.SetActive(true);
            FirstPerson.SetActive(false);
            MiniMap.SetActive(false);
            NormalMode();
        }

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

    private void NormalMode()
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
    }

    private void HandleKeyInput(KeyCode KeyInput, int Direction)
    {
        if (KeyInput != KeyCode.S)
        {
            LockMovement = true;
            Invoke("UnlockMovement", 0.5f);
        }

        currentKey = KeyInput;
        AddDirectionIndex(Direction);
        Rotation();
        Move_Player();
       
    }

    public void HandleKeyInputNormal(int direction)
    {
        currentDirection = Directions[direction];
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

    private void Rotation() // Pacman wordt met deze methode geroteerd naar de richting waar die naar toe gaat.
    {
        Vector3[] RotateList = { new Vector3(0, 90, 0), new Vector3(0, 180, 0), new Vector3(0, 270, 0), new Vector3(0, 0, 0) };

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

    public void StartDeathSequence() // Gebruik deze methode wanneer Pacman de "Enemy" heeft geraakt.
    {
        // SendMessage("DecreaseHealth");
        transform.position = Spawner.transform.position;
        rigidbody.useGravity = true;
        Invoke("DisableGravity", 1f);
    }

    private void UnlockMovement()
    {
        LockMovement = false;
    }

    public void DisableGravity()
    {
        rigidbody.useGravity = false;
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

    public KeyCode CurrentKey
    {
        get { return currentKey; }
    }

    public KeyCode P_Key
    {
        get { return p_Key; }
    }
}
