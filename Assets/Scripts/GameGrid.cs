using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour {

    int range;
    bool LockRotate;

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
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    break;
                case 1:
                    transform.rotation = Quaternion.Euler(0, 0, -18f);
                    break;
                case 2:
                    transform.rotation = Quaternion.Euler(0, 0, 18);
                    break;
                case 3:
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                    break;
            }
        }   
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        LockRotate = !LockRotate;
    }
}
