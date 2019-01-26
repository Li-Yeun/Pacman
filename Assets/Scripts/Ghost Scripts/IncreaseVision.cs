using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IncreaseVision : NetworkBehaviour {

    [SerializeField] float T_Range, T_Intensity, T_Height, Speed;
    [SerializeField] Light lt;
    public bool activated;
    private Transform tr;
    private float Range, Intensity, Height;
    private float[] Ref = new float[3];

    void Start()
    {
        tr = transform;
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
            Invoke("DisableLight", 2.5f);
        }
        else if (activated)
        {
            tr.localPosition = new Vector3(0,Mathf.SmoothDamp(tr.localPosition.y, T_Height, ref Ref[0], Speed),0);
            lt.range = Mathf.SmoothDamp(lt.range, T_Range, ref Ref[1], Speed);
            lt.intensity = Mathf.SmoothDamp(lt.intensity, T_Intensity, ref Ref[2], Speed);
            if (FindObjectOfType<viseonTimer>() != null)
            {
                viseonTimer timerAnimation = FindObjectOfType<viseonTimer>();
                timerAnimation.ViseonTimer();
            }
            
        }

	}

    private void DisableLight()
    {
        gameObject.SetActive(false);
    }
}