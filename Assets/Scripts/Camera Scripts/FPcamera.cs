using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPcamera : MonoBehaviour {
    public Transform tr_Target;
    public Transform tr_Camera;
    public float turnspeed = 0.1f;
    Vector3 Offset = Vector3.zero;
    private Vector3 R_offset = new Vector3(0,-90,0);
    private Quaternion TargetAngle;
	void Update ()
    {
         tr_Camera.position = tr_Target.position + Offset;
         TargetAngle = tr_Target.rotation * Quaternion.Euler(0, R_offset.y, 0);
         tr_Camera.rotation = Quaternion.Slerp(tr_Camera.rotation, TargetAngle, turnspeed * Time.deltaTime);
    }
}
