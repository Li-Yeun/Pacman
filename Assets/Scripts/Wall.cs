using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    BoxCollider boxCollider;
    // Use this for initialization
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                boxCollider.enabled = false;
                Invoke("turnOnCollider", 1);
                break;
            default:
                break;
        }

    }

    void turnOnCollider()
    {
        boxCollider.enabled = true;
    }
}
