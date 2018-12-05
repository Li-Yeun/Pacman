using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson : MonoBehaviour
{
    public GameObject target;
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
        LockMovement = false;

    }

    void LateUpdate()
    {
        if (LockMovement == false)
        {
                                                                                                                                                  if ((Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.S)) || (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.S)) || (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.D)) || (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.D)) || (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D)))
                                                                                                                                                     return;
            else if (Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(RotateMe(Vector3.up * 90, CameraTurnSpeed));
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(RotateMe(Vector3.up * -90, CameraTurnSpeed));

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(RotateMe(Vector3.up * 180, CameraTurnSpeed));
            }

        }
        transform.position = target.transform.position + CameraOffset;

    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        LockMovement = true;
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        transform.rotation = Quaternion.Slerp(fromAngle, toAngle, 1);
        Pacman.LockRotateMovement = false;
        LockMovement = false;

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