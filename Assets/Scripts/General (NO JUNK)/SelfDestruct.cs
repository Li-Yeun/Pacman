using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float time;
    private float timer = 0f;

    void Start()
    {
        if (gameObject.name == "Water Trippy(Clone)")
        {
            Camera[] camera = FindObjectsOfType<Camera>();
            foreach (Camera cam in camera)
            {
                if (cam == null)
                    return;
                else if (cam.name == "Top Down Camera(Clone)")
                    cam.orthographic = false;
            }
        }

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            if (gameObject.name == "Water Trippy(Clone)")
            {
                Camera[] camera = FindObjectsOfType<Camera>();
                foreach (Camera cam in camera)
                {
                    if (cam == null)
                        return;
                    else if (cam.name == "Top Down Camera(Clone)")
                        cam.orthographic = true;
                }
            }
            FindObjectOfType<EnvironementalEvents>().ResetTimer();
            Destroy(gameObject);
        }
    }

    public void Reset()
    {
        if (gameObject.name == "Water Trippy(Clone)")
        {
            Camera[] camera = FindObjectsOfType<Camera>();
            foreach (Camera cam in camera)
            {
                if (cam == null)
                    return;
                else if (cam.name == "Top Down Camera(Clone)")
                    cam.orthographic = true;
            }
        }
        FindObjectOfType<EnvironementalEvents>().ResetTimer();
        Destroy(gameObject);
    }
}
