using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public Rigidbody rb;
    public Transform tf;
    public Trigger front;
    public Trigger back;
    public Trigger left;
    public Trigger right;
    public string[] Controls = new string[4];
    public int speed = 100;
    float cooldown = 0;
    Vector3 Facing;
    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    void FixedUpdate()
    {
        Debug.Log((int)tf.eulerAngles.y);
        MoveForward();
        HandleInput();
        if (front.Collision||back.Collision)
        {
            if (!right.Collision && cooldown == 0)
            {
                Rotate(-1);

            }
            else if (!left.Collision && cooldown == 0)
            {
                Rotate(1); 

            }
        }

    }

    void MoveForward()
    {
        switch ((int)tf.eulerAngles.y)
        {
            case 0:
                Facing = new Vector3(-Time.deltaTime,0,0);
                break;
            case 90:
                Facing = new Vector3(0, 0,Time.deltaTime);
                break;
            case 180:
                Facing = new Vector3(Time.deltaTime, 0,0);
                break;
            case 270:
                Facing = new Vector3(0,0,-Time.deltaTime);
                break;
        }

        tf.position += Facing * speed;    
    }

    void Rotate(int i)
    {
        tf.Rotate(0,i*90,0);
        cooldown = 1;
    }

    void HandleInput()
    {
        if (Input.GetKey(Controls[0]) && cooldown == 0)
        {
            Rotate(1); 
        }
        if (Input.GetKey(Controls[1]) && cooldown == 0)
        {
            Rotate(-1);
        }
        if(cooldown >= 1)
        {
            cooldown += Time.deltaTime;
            if(cooldown >= 1.2f)
            {
                cooldown = 0;
            }
        }
    }
}
