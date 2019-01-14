using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {

    Transform FollowTarget;
    float xOffset;
    float zOffset;
	// Use this for initialization
	void Start () {
        PacmanMovement Target = FindObjectOfType<PacmanMovement>();
        FollowTarget = Target.gameObject.transform;
    }
	
	// Update is called once per frame
    void LateUpdate()
    {
        xOffset = CheckBoundingBox(transform.position.x, FollowTarget.transform.position.x, -7.7f, 13.65f);
        zOffset = CheckBoundingBox(transform.position.z, FollowTarget.transform.position.z, -0.92f, 8.9f);
        transform.position = new Vector3(xOffset, transform.position.y, zOffset);
    }

    private float CheckBoundingBox(float dimensionPos, float targetPos, float min, float max)
    {
        if (dimensionPos != targetPos)
        {
            if (targetPos >= min && targetPos <= max)
            {
                return targetPos;
            }
            else if (targetPos < min)
            {
                return min;
            }
            else if (targetPos > max)
            {
                return max;
            }
        } return dimensionPos;
    }
}
