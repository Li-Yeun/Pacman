using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Class makes the camera follow the target
/// </summary>
public class FollowCamera : MonoBehaviour {

    public GameObject FollowTarget;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void LateUpdate()
    {
        transform.position = new Vector3(FollowTarget.transform.position.x, transform.position.y, FollowTarget.transform.position.z);
	}
}
