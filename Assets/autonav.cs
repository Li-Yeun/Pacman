using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autonav : MonoBehaviour {
    [SerializeField] SpecialTrigger2 front, left, right;               //colliders voor navigatie
    private Vector3 Velocity;
    [SerializeField] int speed = 0;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Autonav();
        MoveForward();
    }
    private void Rotate(int i)
    {
        gameObject.transform.Rotate(0, i * 90, 0);
    }

    private void Autonav()
    {
        if (front.Collision)
        {
            if (!right.Collision)
            {
                Rotate(1);
            }
            else if (!left.Collision)
            {
                Rotate(-1);
            }
            else if (left.Collision)
            {
                Rotate(2);
            }
        }
    }

    private void MoveForward()
    {
        switch ((int)gameObject.transform.eulerAngles.y)
        {
            case 180:
                Velocity = new Vector3(Time.deltaTime, 0, 0);
                break;
            case 270:
                Velocity = new Vector3(0, 0, -Time.deltaTime);
                break;
            case 0:
                Velocity = new Vector3(-Time.deltaTime, 0, 0);
                break;
            case 90:
                Velocity = new Vector3(0, 0, Time.deltaTime);
                break;
        }

        gameObject.transform.position += Velocity * speed;

    }
}
