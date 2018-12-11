using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MiniMap : NetworkBehaviour {

    public Transform FollowTarget;

	// Use this for initialization
	void Start () {
        PacmanMultiMovement Target = FindObjectOfType<PacmanMultiMovement>();
        FollowTarget = Target.gameObject.transform;
    }
	
	// Update is called once per frame
	void LateUpdate() {
        if (!hasAuthority)
            return;
        transform.position = new Vector3(FollowTarget.transform.position.x, transform.position.y, FollowTarget.transform.position.z);
	}
}
