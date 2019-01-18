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
            
            foreach (TeleportReceiver tp in FindObjectsOfType<TeleportReceiver>())
            {
                if (tp.code == code && tp.gameObject.transform.parent.parent.gameObject != this.gameObject)
                {
                    collider.GetComponent<SmoothSync>().teleportAnyObjectFromServer(tp.transform.position, collider.transform.rotation, collider.transform.localScale);
                }
            }
        }
    }

    void OnTriggerExit (Collider collider)
    {
        this.gameObject.GetComponent<TeleportScript2>().Lockk = false;
    }
}