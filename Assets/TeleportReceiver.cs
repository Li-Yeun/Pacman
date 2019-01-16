using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportReceiver : MonoBehaviour {

    public int code;

	// Use this for initialization
	void Start ()
    {
        code = GetComponentInParent<TeleportScript2>().code;
	}
}
