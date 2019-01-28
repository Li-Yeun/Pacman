using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {

    private Transform FollowTarget;
    private float xOffset, zOffset;

    void LateUpdate()
    {
        if (FindObjectOfType<PacmanMovement>() == null)
            return;
        else
        {
            PacmanMovement Target = FindObjectOfType<PacmanMovement>();
            FollowTarget = Target.gameObject.transform;
        }
        xOffset = CheckBoundingBox(transform.position.x, FollowTarget.transform.position.x, -7.7f, 13.65f);
        zOffset = CheckBoundingBox(transform.position.z, FollowTarget.transform.position.z, -0.92f, 8.9f);
        transform.position = new Vector3(xOffset, transform.position.y, zOffset);
    }


    // Checken of de camera binnen de muren van de doolhof zit, zoniet dan staat de camera postie van die as stil
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
