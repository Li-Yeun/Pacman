﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPcamera : MonoBehaviour {
    public Transform tr_Target;
    public float turnspeed = 0.1f;
    Vector3 Offset = Vector3.zero;
    private Vector3 R_offset = new Vector3(0,-90,0);
    private Quaternion TargetAngle;
	void Update ()
    {
         gameObject.transform.position = tr_Target.position + Offset;
         TargetAngle = tr_Target.rotation * Quaternion.Euler(0, R_offset.y, 0);
         gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, TargetAngle, turnspeed * Time.deltaTime);
    }
}