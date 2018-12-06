using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour {

    public GameObject target;
    public PacmanMovement pacmanMovement;
    [SerializeField] float CameraTurnSpeed = 0.1f;
    public Vector3 CameraOffset;
    void Start()
    {
        RotateCamera();
        transform.position = target.transform.position + CameraOffset;
    }

    void LateUpdate()
    {
        if(pacmanMovement.CurrentDirection != pacmanMovement.P_Direction)
        {
            RotateCamera();
        }
        transform.position = target.transform.position + CameraOffset;

    }

    public void RotateCamera()
    {
        if(pacmanMovement.CurrentKey == KeyCode.S)
        {
            StartCoroutine(RotateMe(Vector3.up * 180, CameraTurnSpeed));
        } else if(pacmanMovement.CurrentKey == KeyCode.A)
        {
            StartCoroutine(RotateMe(Vector3.up * -90, CameraTurnSpeed));
        } else if((pacmanMovement.CurrentKey == KeyCode.D))
        {
            StartCoroutine(RotateMe(Vector3.up * 90, CameraTurnSpeed));
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
        switch (pacmanMovement.CurrentDirection)
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

}
