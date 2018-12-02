using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Movement : MonoBehaviour {
    public Rigidbody rb;    
    public Transform tf;    
    public Trigger front, left, right;               //colliders voor navigatie
    public string[] Controls = new string[2];        //controlls van spookje
    public int speed = 100;                          //beweegsnelheid
    public int Respawntime = 4;                      //tijd voordat speler weer kan besturen
    float RotationCooldown = 0;                      //tijd voordat er weer gedraaid kan worden
    Vector3 Velocity;                                //beweegsnelheid
    public NavMeshAgent agent;                       //navigatie voor teruggaan naar spawn
    public Transform respawn;                        //locatie van spawnpunt
    public bool dead = false;                        //als true is de speler doodgegaan
    float respawntimer = 0;         

    void Start ()
    {
        agent.enabled = false;
	}
	
	void Update ()
    {
        if (dead)
        {
            DoRespawn();
        }
	}

    void FixedUpdate()
    {
        if (!dead)
        {
            MoveForward();
            HandleInput();
            AutoNav();
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
    }       //beweegt Character naar de kijkrichting
    void Rotate(int i)
    {
        tf.Rotate(0,i*90,0);
        RotationCooldown = 1;
    }       //Roteer de Character met 90 graden naar 1 rechts, -1 links
    void DoRespawn()
    {
        agent.enabled = true;
        respawntimer += Time.deltaTime;
        if (respawntimer >= Respawntime)
        {
            dead = false;
            agent.enabled = false;
            tf.eulerAngles = new Vector3(0, 0, 0);
            respawntimer = 0;

        }
        agent.SetDestination(respawn.position);
    }         //roept NavMeshAgent aan om de character naar spawn te sturen
    void AutoNav()
    {
        if (front.Collision)
        {
            if (!right.Collision)
            {
                Rotate(1);

            }
            else if (!left.Collision)
            {
                Rotate(-1);

            }
            else if (left.Collision)
            {
                Rotate(2);
            }
        }
    }           //zorgt voor automatisch draaien tegen muren
    void HandleInput()
    {
        if (Input.GetKey(Controls[0]) && RotationCooldown == 0 && !right.Collision)
        {
            Rotate(1); 
        }
        if (Input.GetKey(Controls[1]) && RotationCooldown == 0 && !left.Collision)
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
    }       //neemt de twee inputs om te draaien, zorgt ook dat je niet een muur in kan draaien. Bevat ook draai cooldown
}
