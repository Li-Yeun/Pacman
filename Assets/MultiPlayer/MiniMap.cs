using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MiniMap : NetworkBehaviour {

    Transform FollowTarget;

	// Use this for initialization
	void Start () {
        PacmanMovement Target = FindObjectOfType<PacmanMovement>();
        FollowTarget = Target.gameObject.transform;
    }
	
	// Update is called once per frame
	void LateUpdate() {
        transform.position = new Vector3(FollowTarget.transform.position.x, transform.position.y, FollowTarget.transform.position.z);
	}
}
