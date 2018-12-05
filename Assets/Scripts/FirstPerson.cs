using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour {

    public GameObject target;
    public PacmanMovement pacmanMovement;
    public float damping = 1;
    Vector3 offset;
    public float angleOffset;
    public float distanceOffset;
    public static bool LockMovement;
    [SerializeField] float CameraTurnSpeed = 0.1f;
    public List<Light> Lights;
    public Vector3 CameraOffset;
    void Start()
    {
        RotateCamera();
        transform.position = target.transform.position + CameraOffset;
    }

    void LateUpdate()
    {
        if(pacmanMovement.currentDirection != pacmanMovement.p_Direction)
        {
            RotateCamera();
        }
        transform.position = target.transform.position + CameraOffset;

    }

    public void RotateCamera()
    {
        switch(pacmanMovement.CurrentDirection)
        {
            case "Up":
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            case "Right":
                transform.localRotation = Quaternion.Euler(0, 90, 0);
                break;
            case "Down":
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                break;
            case "Left":
                transform.localRotation = Quaternion.Euler(0, 270, 0);
                break;

        }
    }
    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        transform.rotation = Quaternion.Slerp(fromAngle, toAngle, 1);
    }

    void OnPreRender()
    {
        foreach (Light light in Lights)
        {
            light.enabled = false;
        }
    }

    void OnPostRender()
    {
        foreach (Light light in Lights)
        {
            light.enabled = true;
        }
    }
}
