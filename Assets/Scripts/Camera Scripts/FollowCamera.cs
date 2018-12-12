using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    private GameObject FollowTarget;

	void Start () {
        FollowTarget = FindObjectOfType<PacmanMovement>().gameObject;
	}
	
	void LateUpdate() {
        transform.position = new Vector3(FollowTarget.transform.position.x, transform.position.y, FollowTarget.transform.position.z);
	}
}
