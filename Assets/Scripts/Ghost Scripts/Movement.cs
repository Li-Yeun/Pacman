using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Insert alle functies van wat deze classe doet. Thanks.
/// </summary>

public class Movement : MonoBehaviour {

    [Header("General")]
    [SerializeField] Trigger front, left, right;               //colliders voor navigatie
    [SerializeField] NavMeshAgent agent;                       //navigatie voor teruggaan naar spawn
    [SerializeField] Transform respawn;                        //locatie van spawnpunt

    [Header("Ghost Behaviour")]
    [SerializeField] int speed = 100;                          //beweegsnelheid
    [SerializeField] int Respawntime = 4;                      //tijd voordat speler weer kan besturen

    [Header("Control Options")]
    [SerializeField] string[] Controls = new string[2];        //controlls van spookje

    Vector3 Velocity;                                          //beweegsnelheid
    bool dead = false;                                         //als true is de speler doodgegaan
    bool UseRandom;
    float RotationCooldown = 0;                                //tijd voordat er weer gedraaid kan worden
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
        switch ((int)gameObject.transform.eulerAngles.y)
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

        gameObject.transform.position += Velocity * speed;    
    }       //beweegt Character naar de kijkrichting
    void Rotate(int i)
    {
        gameObject.transform.Rotate(0,i*90,0);
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
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            respawntimer = 0;

        }
        agent.SetDestination(respawn.position);
    }         //roept NavMeshAgent aan om de character naar spawn te sturen
    void AutoNav()
    {
        if (front.Collision)
        {
            //Ik weet niet waarom dit niet altijd goed werkt
            //if (!right.Collision && !left.Collision)  
            //{
            //    int side = Random.Range(0, 2)*2-1;
                
            //    Debug.Log(side);
            //    Rotate(side);
            //}
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
