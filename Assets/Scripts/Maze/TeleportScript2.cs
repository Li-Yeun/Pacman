using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Smooth;

/// <summary>
/// Script dat zorgt dat de teleporters werken.
/// </summary>
public class TeleportScript2 : MonoBehaviour
{
    public int code;
    public bool Lockk = false ;

    void OnTriggerEnter(Collider collider)
    {

        if ((collider.CompareTag("Player") || collider.CompareTag("Enemy") || collider.CompareTag("Decoy")) && !Lockk )
        {
            
            foreach (TeleportScript2 tp in FindObjectsOfType<TeleportScript2>())
            {
                if (tp.code == code && tp != this)
                {
                    tp.Lockk = true;
                    Debug.Log(2);
                    collider.GetComponent<SmoothSync>().teleportAnyObjectFromServer(tp.transform.position, collider.transform.rotation, collider.transform.localScale);
 
                }
            }
        }
    }

    void OnTriggerExit (Collider collider)
    {
        if ((collider.CompareTag("Player") || collider.CompareTag("Enemy") || collider.CompareTag("Decoy")))
        {
            this.gameObject.GetComponent<TeleportScript2>().Lockk = false;
        }
    }
}