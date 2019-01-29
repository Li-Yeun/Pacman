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
                    cam.orthographic = false;           // Het camera perspectief van 3D naar 2D veranderen, is nodig om specifieke effects te kunnen zien
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
                        cam.orthographic = true;        // Het camera perspectief van 2D naar 3D veranderen
                }
            }

            if(FindObjectOfType<EnvironementalEvents>() != null)
                FindObjectOfType<EnvironementalEvents>().ResetTimer();

            // Het object wordt hier vernietigd als de timer een bepaalde tijd heeft bereikt
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
                    cam.orthographic = true;                    // Het camera perspectief naar 2D veranderen als dat nog niet gebeurd is
            }
        }

        if (FindObjectOfType<EnvironementalEvents>() != null)
            FindObjectOfType<EnvironementalEvents>().ResetTimer();

        Destroy(gameObject);
    }
}
