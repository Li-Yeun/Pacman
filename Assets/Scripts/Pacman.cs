using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour {

    [SerializeField] float xspd;
    [SerializeField] float yspd;
    [SerializeField] float zspd;

    [SerializeField] float xspd2;
    [SerializeField] float yspd2;
    [SerializeField] float zspd2;

    public GameObject NormalCamera, ThirdPerson, MiniMap, MultiPlayerCamera, EnemyCamera;
    enum direction {up,down,left,right, none};
    direction[] Directions;
    Quaternion[] cameraList;
    Quaternion currentCamera;
    direction currentDirection;
    int directionIndex, cameraIndex;
    float rotate, rotatevalue, z_angle;
    public static Vector3 Position;
    bool SwitchControls;
    bool LockMovement;
    public static bool LockRotateMovement;
    [SerializeField] float LockRotateTime;


    Rigidbody rigidbody;
    public static KeyCode currentKey;

    // Use this for initialization
    void Start () {

        rigidbody = GetComponent<Rigidbody>();
        Directions = new direction[]{ direction.up, direction.right, direction.down, direction.left };
        cameraList = new Quaternion[] { new Quaternion(0, 0, -1.455192e-11f, 1), new Quaternion(1.028975e-11f, 0.7071065f, -1.028976e-11f, 0.7071071f), new Quaternion(1.455192e-11f, 1, -1.214306e-17f, 9.834765e-07f), new Quaternion(1.028977e-11f, 0.7071075f, 1.028975e-11f, -0.7071061f) };
        cameraIndex = 0;
        currentCamera = cameraList[cameraIndex];
        directionIndex = 0;
        rotate = 0;
        rotatevalue = 0;
        currentDirection = Directions[directionIndex];
        SwitchControls = false;
        LockMovement = false;
        LockRotateMovement = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (LockMovement == false)
        {
            KeyInput();
        }
        Position = transform.position;

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

    private void ApplyLock()
    {
        LockRotateMovement = false;
    }

    private void StartDeathSequence()
    {
        transform.position = new Vector3(0, 0, 0);
       
    }

    void KeyInput()
    {
            if (Input.GetKeyDown(KeyCode.P))
            {
            MultiPlayerCamera.SetActive(false);
            EnemyCamera.SetActive(false);
            SwitchControls = !SwitchControls;
                
        } else if (Input.GetKeyDown(KeyCode.M))
        {
            NormalCamera.SetActive(false);
            ThirdPerson.SetActive(false);
            MiniMap.SetActive(false);
            MultiPlayerCamera.SetActive(true);
            EnemyCamera.SetActive(true);
            ThridPerson();
        }

            if (SwitchControls)
            {
                NormalCamera.SetActive(false);
                ThirdPerson.SetActive(true);
                MiniMap.SetActive(true);
                ThridPerson();

            }
            else
            {
                NormalCamera.SetActive(true);
                ThirdPerson.SetActive(false);
                MiniMap.SetActive(false);
                NormalMode();
            }

    }

    private void ThridPerson()
    {
        if (LockRotateMovement == true)
            return;

        if ((Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.S)) || (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.S)) || (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.D)) || (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.D)) || (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D)))
            return;
        else if (Input.GetKeyDown(KeyCode.W))
            {
                Move_Player();

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
            currentKey = KeyCode.S;
            LockRotateMovement = true;
            z_angle = 10;
                AddDirectionIndex(2);
                Move_Player();
            //    StartCoroutine(RotateMe(Vector3.up * 180, 0.1f));

            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
            currentKey = KeyCode.A;
            LockRotateMovement = true;
            z_angle = -10;
                AddDirectionIndex(-1);
                Move_Player();
               // StartCoroutine(RotateMe(Vector3.up * -90, 0.1f));
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
            currentKey = KeyCode.D;
            LockRotateMovement = true;
            z_angle = 20;
                AddDirectionIndex(1);

                Move_Player();
               // StartCoroutine(RotateMe(Vector3.up * 90, 0.1f));
            }
        
    }

    private void NormalMode()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            rigidbody.velocity = new Vector3(0, 0, zspd);
            rigidbody.rotation = cameraList[0];
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            rigidbody.velocity = new Vector3(0, 0, -zspd);
            rigidbody.rotation = cameraList[2];
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            rigidbody.velocity = new Vector3(-xspd, 0, 0);
            rigidbody.rotation = cameraList[3];
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            rigidbody.velocity = new Vector3(xspd, 0, 0);
            rigidbody.rotation = cameraList[1];
        }
    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles + new Vector3(0,10,0));
        LockMovement = true;
        for (var t = 0f; t < 1; t += inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        LockMovement = false;
    }


    public void AddDirectionIndex(int number)
    {
        directionIndex += number;
        if(directionIndex > Directions.Length-1)
        {
            directionIndex -= Directions.Length;
        } else if(directionIndex < 0)
        {
            directionIndex = 3;
        }
        currentDirection = Directions[directionIndex];
    }
    public void Move_Player()
    {
        if(currentDirection == direction.up)
        {
            rigidbody.velocity = new Vector3(0, 0, zspd);
        }
        else if(currentDirection == direction.right)
        {
            rigidbody.velocity = new Vector3(xspd, 0, 0);
        }
        else if (currentDirection == direction.down)
        {
            rigidbody.velocity = new Vector3(0, 0, -zspd);
        }
        else if (currentDirection == direction.left)
        {
            rigidbody.velocity = new Vector3(-xspd, 0, 0);
        }
    }
}

