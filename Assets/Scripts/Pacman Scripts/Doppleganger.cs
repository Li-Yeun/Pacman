using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;
/// <summary>
/// Insert alle functies van wat deze classe doet. Thanks.
/// </summary>

public class Doppleganger : NetworkBehaviour {

    [Header("General")]
    [SerializeField] SpecialTrigger2 front, left, right;               //colliders voor navigatie

    [Header("Ghost Behaviour")]
    [SerializeField] public int speed = 100;                          //beweegsnelheid
    [SerializeField] int Respawntime = 4;                      //tijd voordat speler weer kan besturen

    Vector3 Velocity;                                          //beweegsnelheid
    float RotationCooldown = 0;                                //tijd voordat er weer gedraaid kan worden

    float SpeedMultiplier = 1;

    void Start ()
    {
        Rotate(0);
        
    }

    
    void Update ()
    {
        MoveForward();
        AutoNav();

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
}
