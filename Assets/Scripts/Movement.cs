using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	}
	
	void Update ()
    {
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
	}

    void FixedUpdate()
    {
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
            }
        }

    }

    void MoveForward()
    {
        switch ((int)tf.eulerAngles.y)
        {
            case 0:
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
    }

    void Rotate(int i)
    {
        tf.Rotate(0,i*90,0);
        RotationCooldown = 1;
    }

    void HandleInput()
    {
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
            }
        }
    }
}
