using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD

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
		
=======
using UnityEngine.AI;
public class Movement : MonoBehaviour {
    public Rigidbody rb;    
    public Transform tf;
    public Trigger front, left, right;      //colliders voor navigatie
    public string[] Controls = new string[2];       //controlls van spookje
    public int speed = 100;     //beweegsnelheid
    public int Respawntime = 4;         //tijd voordat speler weer kan besturen
    float RotationCooldown = 0;         //tijd voordat er weer gedraaid kan worden
    Vector3 Velocity;         //beweegsnelheid
    public NavMeshAgent agent;          //navigatie voor teruggaan naar spawn
    public Transform respawn;           //locatie van spawnpunt
    public bool dead = false;           //als true is de speler doodgegaan
    float respawntimer = 0;         

    void Start ()
    {
        agent.enabled = false;
>>>>>>> master
	}
	
	void Update ()
    {
<<<<<<< HEAD
		
=======
        if (dead)
        {
            agent.enabled = true;
            respawntimer += Time.deltaTime;
            if(respawntimer >= Respawntime)
            {
                dead = false;
                agent.enabled = false;
                tf.eulerAngles = new Vector3(0, 0, 0);
                respawntimer = 0;
                
            }
            agent.SetDestination(respawn.position);

        }
>>>>>>> master
	}

    void FixedUpdate()
    {
<<<<<<< HEAD
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

=======
        if (!dead)
        {
            MoveForward();
            HandleInput();
            if (front.Collision)
            {
                if (!right.Collision && RotationCooldown == 0)
                {
                    Rotate(1);

                }
                else if (!left.Collision && RotationCooldown == 0)
                {
                    Rotate(-1);

                }
                else if (left.Collision && right.Collision)
                {
                    Rotate(2);
                }
>>>>>>> master
            }
        }

    }

    void MoveForward()
    {
        switch ((int)tf.eulerAngles.y)
        {
            case 0:
<<<<<<< HEAD
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
=======
                Velocity = new Vector3(Time.deltaTime,0,0);
                break;
            case 90:
                Velocity = new Vector3(0, 0,-Time.deltaTime);
                break;
            case 180:
                Velocity = new Vector3(-Time.deltaTime, 0,0);
                break;
            case 270:
                Velocity = new Vector3(0,0,Time.deltaTime);
                break;
        }

        tf.position += Velocity * speed;    
>>>>>>> master
    }

    void Rotate(int i)
    {
        tf.Rotate(0,i*90,0);
<<<<<<< HEAD
        cooldown = 1;
=======
        RotationCooldown = 1;
>>>>>>> master
    }

    void HandleInput()
    {
<<<<<<< HEAD
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
=======
        if (Input.GetKey(Controls[0]) && RotationCooldown == 0)
        {
            Rotate(1); 
        }
        if (Input.GetKey(Controls[1]) && RotationCooldown == 0)
        {
            Rotate(-1);
        }
        if(RotationCooldown >= 1)
        {
            RotationCooldown += Time.deltaTime;
            if(RotationCooldown >= 1.2f)
            {
                RotationCooldown = 0;
>>>>>>> master
            }
        }
    }
}
