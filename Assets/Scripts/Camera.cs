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


    public List<Light> Lights;
    // Use this for initialization

    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        CameraOffset = new Vector3(cameraX, cameraY, cameraZ);
    }
    void OnPreRender()
    {
        foreach (Light light in Lights)
        {
            light.enabled = true;
        }
    }

    void OnPostRender()
    {
        foreach (Light light in Lights)
        {
            light.enabled = false;
        }
    }
}
