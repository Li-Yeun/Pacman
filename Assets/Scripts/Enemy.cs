using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float xspd;
    [SerializeField] float yspd;
    [SerializeField] float zspd;


    Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {

        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyInput();
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void CollisionOff()
    {
    }

    void KeyInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigidbody.AddForce(0, 0, zspd);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidbody.AddForce(0, 0, -zspd);

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.AddForce(-xspd, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.AddForce(xspd, 0, 0);
        }
    }
}