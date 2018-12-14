using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script dat zorgt dat de teleporters werken.
/// </summary>
public class TeleportScript2 : MonoBehaviour {
    public int code = 0;
    float dissableTimer =0;
    
    void OnTriggerEnter(Collider collider)
    {
        if ((collider.gameObject.name == "Pacman" || collider.gameObject.name == "Blue") && dissableTimer <=0)
        {
            foreach (TeleportScript2 tp in FindObjectsOfType<TeleportScript2>())
            {   
                if (tp.code == code && tp != this)
                {
                    tp.dissableTimer = 1;
                    Vector3 position = tp.gameObject.transform.position;
                    position.y += 0.2f;
                    collider.gameObject.transform.position = position;
                }
            }
        }
    }
	// Update is called once per frame
	void Update ()
    {
        if (dissableTimer > 0)
        {
            dissableTimer -= Time.deltaTime;
        }
	}
}
