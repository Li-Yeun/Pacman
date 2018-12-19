using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script dat zorgt dat de teleporters werken.
/// </summary>
public class TeleportScript2 : MonoBehaviour
{
    PacmanMovement pacmanMovement;
    TeleportScript2 Teleportshit;
    private void Start()
    {
        pacmanMovement = FindObjectOfType<PacmanMovement>();
    }
    public int code;
    private bool Lockk = false ;
    void OnTriggerEnter(Collider collider)
    {
        if ((collider.gameObject.name == "Pacman" || collider.gameObject.name == "Blue") && !Lockk )
        {
            foreach (TeleportScript2 tp in FindObjectsOfType<TeleportScript2>())
            {
                if (tp.code == code && tp != this)
                {
                    tp.Lockk = true;
                    collider.transform.position = tp.transform.position;
                    Invoke("Relock", 0.5f);
                    Teleportshit = tp;
                }
            }
        }
    }
    void Relock()
    {
        Teleportshit.Lockk = false;
    }
}