using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IncreaseVision : NetworkBehaviour {
    public bool activated;
    public Transform tr;
    public Light lt;
    float Range;
    float Intensity;
    float Height;
    float[] Ref = new float[3];

    public float T_Range;
    public float T_Intensity;
    public float T_Height;
    public float Speed;
    void Start()
    {
        Range = lt.range;
        Intensity = lt.intensity;
        Height = tr.localPosition.y;
    }
	void Update ()
    {
        if (!activated)
        {
            tr.localPosition = new Vector3(0, Mathf.SmoothDamp(tr.localPosition.y, Height, ref Ref[0], Speed), 0);
            lt.range = Mathf.SmoothDamp(lt.range, Range, ref Ref[1], Speed);
            lt.intensity = Mathf.SmoothDamp(lt.intensity, Intensity, ref Ref[2], Speed);
        }
        else if (activated)
        {
            tr.localPosition = new Vector3(0,Mathf.SmoothDamp(tr.localPosition.y, T_Height, ref Ref[0], Speed),0);
            lt.range = Mathf.SmoothDamp(lt.range, T_Range, ref Ref[1], Speed);
            lt.intensity = Mathf.SmoothDamp(lt.intensity, T_Intensity, ref Ref[2], Speed);
        }

	}

    
}