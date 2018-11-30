using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour {

    int range;
    bool LockRotate;
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            range = Random.Range(0, 3);
            RotateField();
        }
	}

    private void RotateField()
    {
        if (!LockRotate)
        {
            switch (range)
            {
                case 0:
                    transform.Rotate(0, 0, 15.7f);
                    transform.position = new Vector3(-6.4f, transform.position.y, -7.807534f);
                    break;
                case 1:
                    transform.Rotate(0, 0, -15.7f);
                    transform.position = new Vector3(6.4f, transform.position.y, -7.807534f);
                    break;
                case 2:
                    transform.Rotate(20, 0, 0);
                    transform.position = new Vector3(-0.6253641f, transform.position.y, 0.78f);
                    break;

                case 3:
                    transform.Rotate(-20, 0, 0);
                    transform.position = new Vector3(-0.6253641f, transform.position.y, -0.78f);
                    break;
            }
        }

        
        else
        {
            transform.rotation = new Quaternion(0f, 0f, 0f,0f);
            transform.position = new Vector3(-0.6253641f, transform.position.y, -7.807534f);
        }

        LockRotate = !LockRotate;


    }
}
