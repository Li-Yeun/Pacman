using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;
using Smooth;
using System;

/// <summary>
/// Insert alle functies van wat deze classe doet. Thanks.
/// </summary>

public class Movement : NetworkBehaviour {

    public string name; //Checken welke ghost er bestaan bij character selectie;

    [Header("General")]
    [SerializeField] SpecialTrigger2 front, left, right;               //colliders voor navigatie

    [Header("Ghost Behaviour")]
    [SerializeField] public float speed;                        //beweegsnelheid
    [SerializeField] int Respawntime = 4;                      //tijd voordat speler weer kan besturen

    [Header("Control Options")]
    [SerializeField] string[] Controls = new string[2];        //controls van spookje

    [SerializeField] GameObject IncreaseVisionLight;
    [SerializeField] IncreaseVision Vision;   //extern script van ability 0, zit op bodylight
    [SerializeField] Animation animation;

    public bool dead = false, reversecontrols = false;          //als true is de speler doodgegaan
    public float[] Cooldown, Duration;        //instelbare cooldown per ability || instelbare Duration per ability

    private bool goleft, goright;
    private float RotationCooldown = 0, respawntimer = 0, SpeedMultiplier = 1, defaulthSpeed;    //tijd voordat er weer gedraaid kan worden
    private Transform respawn;
    private Vector3 Velocity;                                          //beweegsnelheid
    private bool[] Abilities;               //welke ability geactiveerd is
    private float[] cooldowncounter, DurationCounter;        //counter die loopt als ability niet geactiveerd is  || duration counter hoelang de ability geactiveerd is


    void Start ()
    {
        gameObject.transform.parent = GameObject.FindGameObjectWithTag("Ghost Parent").transform;
        respawn = GameObject.FindGameObjectWithTag("Respawn").transform;
        Abilities = new bool[4];
        cooldowncounter = new float[4];
        DurationCounter = new float[4];
        FindObjectOfType<General>().GhostBroadcast();
        gameObject.transform.position = respawn.position;
        defaulthSpeed = speed;
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
        if (!hasAuthority)
            return;
        if (!dead)
        {
            MoveForward();
            HandleInput();
            AutoNav();
        }
    }

    private void MoveForward()
    {
        switch ((int)gameObject.transform.eulerAngles.y)
        {
            case 0:
                Velocity = new Vector3(Time.deltaTime,0,0);
                if (Input.GetKey(Controls[2])) { goleft = true; }
                else { goleft = false; }
                if (Input.GetKey(Controls[0])) { goright = true; }
                else { goright = false; }
                break;
            case 90:
                Velocity = new Vector3(0, 0,-Time.deltaTime);
                if (Input.GetKey(Controls[1])) { goleft = true; }
                else { goleft = false; }
                if (Input.GetKey(Controls[3])) { goright = true; }
                else { goright = false; }
                break;
            case 180:
                Velocity = new Vector3(-Time.deltaTime, 0,0);
                if (Input.GetKey(Controls[0])) { goleft = true; }
                else { goleft = false; }
                if (Input.GetKey(Controls[2])) { goright = true; }
                else { goright = false; }
                break;
            case 270:
                Velocity = new Vector3(0,0,Time.deltaTime);
                if (Input.GetKey(Controls[3])) { goleft = true; }
                else { goleft = false; }
                if (Input.GetKey(Controls[1])) { goright = true; }
                else { goright = false; }
                break;
        }

        gameObject.transform.position += Velocity * speed * SpeedMultiplier;    
    }       //beweegt Character naar de kijkrichting
    private void Rotate(int i)
    {
        gameObject.transform.Rotate(0,i*90,0);
        RotationCooldown = 1;
    }       //Roteer de Character met 90 graden naar 1 rechts, -1 links
    private void DoRespawn()
    {
        respawntimer += Time.deltaTime;
        if (respawntimer >= Respawntime)
        {
            dead = false;
            GetComponent<SmoothSync>().teleportAnyObjectFromServer(respawn.transform.position, Quaternion.Euler(0,0,0), gameObject.transform.localScale);
            respawntimer = 0;
        }
    }
    private void AutoNav()
    {
        if (name == "Pink")
        {
            if (!GetComponent<WalkThroughWalls>().GhostWalking && RotationCooldown == 0)
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
            }
        }
        else
        {
            if (RotationCooldown == 0)
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
            }
        }
    }           //zorgt voor automatisch draaien tegen muren
    private void HandleInput()
    {
        if (reversecontrols == false)
        {
            if (goleft && RotationCooldown == 0 && !right.Collision)
            {
                Rotate(1);
            }
            else if (goright && RotationCooldown == 0 && !left.Collision)
            {
                Rotate(-1);
            }
        }
        else
        {
            if (goleft && RotationCooldown == 0 && !left.Collision)
            {
                Rotate(-1);
            }
            else if (goright && RotationCooldown == 0 && !right.Collision)
            {
                Rotate(1);
            }
        }

        if (RotationCooldown >= 1)
        {
            RotationCooldown += Time.deltaTime;
            if (RotationCooldown >= 1.2f)
            {
                RotationCooldown = 0;
            }
        }
        if (Input.GetKey(KeyCode.Space) && !Abilities[0] && cooldowncounter[0] >= Cooldown[0] && name == "Blue")
        {
            Abilities[0] = true;
        }
        else if (Input.GetKey(KeyCode.Space) && !Abilities[1] && cooldowncounter[1] >= Cooldown[1] && (name == "Red")) //todo Orange weghalen en invisible ability geven
        {
            Abilities[1] = true;
            speedTimer timerAnimation = FindObjectOfType<speedTimer>();
            timerAnimation.SpeedTimer();
            GetComponentInChildren<ParticleSystem>().Play();
        }
    }
    private void DoAbilities()
    {
        if (Abilities[0])
        {
            IncreaseVisionLight.SetActive(true);
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
            SpeedMultiplier = 1.5f;
            cooldowncounter[1] = 0;
            DurationCounter[1] += Time.deltaTime;
            if(DurationCounter[1] >= Duration[1])
            {
                GetComponentInChildren<ParticleSystem>().Stop();
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
        reversecontrols = false;
        speed = defaulthSpeed;
        Camera[] camera = FindObjectsOfType<Camera>();
        foreach (Camera cam in camera)
        {
            if (cam == null)
                return;
            else if (cam.name == "Top Down Camera(Clone)")
                cam.orthographic = true;
        }
        DoRespawn();
    }
}
