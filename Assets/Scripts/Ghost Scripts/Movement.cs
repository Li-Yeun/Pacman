using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;
/// <summary>
/// Insert alle functies van wat deze classe doet. Thanks.
/// </summary>

public class Movement : NetworkBehaviour {

    [Header("General")]
    [SerializeField] SpecialTrigger2 front, left, right;               //colliders voor navigatie
    [SerializeField] NavMeshAgent agent;                       //navigatie voor teruggaan naar spawn
    private Transform respawn;                        //locatie van spawnpunt

    [Header("Ghost Behaviour")]
    [SerializeField] public int speed = 100;                          //beweegsnelheid
    [SerializeField] int Respawntime = 4;                      //tijd voordat speler weer kan besturen

    [Header("Control Options")]
    [SerializeField] string[] Controls = new string[2];        //controls van spookje

    Vector3 Velocity;                                          //beweegsnelheid
    public bool dead = false;                                         //als true is de speler doodgegaan
    float RotationCooldown = 0;                                //tijd voordat er weer gedraaid kan worden
    float respawntimer = 0;


    bool[] Abilities;               //welke ability geactiveerd is
    public IncreaseVision Vision;   //extern script van ability 0, zit op bodylight
    public float[] Cooldown;        //instelbare cooldown per ability
    float[] cooldowncounter;        //counter die loopt als ability niet geactiveerd is
    float[] DurationCounter;        //duration counter hoelang de ability geactiveerd is
    public float[] Duration;        //instelbare Duration per ability
    float SpeedMultiplier = 1;

    void Start ()
    {
        gameObject.transform.parent = GameObject.FindGameObjectWithTag("Ghost Parent").transform;
        respawn = GameObject.FindGameObjectWithTag("Respawn").transform;
        agent.enabled = false;
        Abilities = new bool[4];
        cooldowncounter = new float[4];
        DurationCounter = new float[4];
        FindObjectOfType<General>().GhostBroadcast();
        gameObject.transform.position = respawn.position;
    }

    
    void Update ()
    {
        if (!hasAuthority)
            return;
        if (dead)
        {
            DoRespawn();
        }     

        DoAbilities();
        
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

        gameObject.transform.position += Velocity * speed * SpeedMultiplier;    
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
            gameObject.transform.position = respawn.position;
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


        //TODO forloop van maken :/
        if (Input.GetKey(Controls[2]) && !Abilities[0] && cooldowncounter[0] >= Cooldown[0])
        {
            Abilities[0] = true;
        }
        if (Input.GetKey(Controls[3]) && !Abilities[1] && cooldowncounter[1] >= Cooldown[1])
        {
            Abilities[1] = true;
        }
        if (Input.GetKey(Controls[4]) && !Abilities[2] && cooldowncounter[2] >= Cooldown[2])
        {
            Abilities[2] = true;
        }
    }

    void DoAbilities()
    {
        if (Abilities[0])
        {
            Vision.activated = true;
            cooldowncounter[0] = 0;
            DurationCounter[0] += Time.deltaTime;
            if(DurationCounter[0] >= Duration[0])
            {
                Abilities[0] = false;
                Vision.activated = false;
                DurationCounter[0] = 0;
            }
        }
        else if (!Abilities[0])
        {
            cooldowncounter[0] += Time.deltaTime;
        }

        if (Abilities[1])
        {
            SpeedMultiplier = 1.3f;
            cooldowncounter[1] = 0;
            DurationCounter[1] += Time.deltaTime;
            if(DurationCounter[1] >= Duration[1])
            {
                Abilities[1] = false;
                SpeedMultiplier = 1;
                DurationCounter[1] = 0;
            }
        }
        else if (!Abilities[1])
        {
            cooldowncounter[1] += Time.deltaTime;
        }

    }

    public void Reset()
    {
        DoRespawn();
    }
}
