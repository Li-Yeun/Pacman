using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    [SerializeField] float cameraX;
    [SerializeField] float cameraY;
    [SerializeField] float cameraZ;


    [SerializeField] float xrotate;
    [SerializeField] float yrotate;
    [SerializeField] float zrotate;
    Vector3 CameraOffset;

    // Update is called once per frame
    void Update()
    {
        CameraOffset = new Vector3(cameraX, cameraY, cameraZ);
    }
}
