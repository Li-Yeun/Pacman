using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FPMulticamera : NetworkBehaviour
{
    private Transform tr_Target;
    public float turnspeed = 0.1f;
    Vector3 Offset = Vector3.zero;
    private Vector3 R_offset = new Vector3(0,-90,0);
    private Quaternion TargetAngle;

    private void Start()
    {
        PacmanMultiMovement Target = FindObjectOfType<PacmanMultiMovement>();
        tr_Target = Target.gameObject.transform;
    }

    void Update ()
    {
        if (!hasAuthority)
            return;
         gameObject.transform.position = tr_Target.position + Offset;
         TargetAngle = tr_Target.rotation * Quaternion.Euler(0, R_offset.y, 0);
         gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, TargetAngle, turnspeed * Time.deltaTime);
    }
}