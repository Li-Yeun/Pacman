using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                    collider.transform.position = tp.transform.position;
                }
            }
        }
    }

    void OnTriggerExit (Collider collider)
    {
        this.gameObject.GetComponent<TeleportScript2>().Lockk = false;
    }
}