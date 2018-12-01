using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activelevel : MonoBehaviour {
    public GameObject[] GameObject;
    public int active = 0;
	void Start ()
    {
        if(active >= GameObject.Length)
        {
            active = GameObject.Length - 1;
        }

        for (int i = 0; i < GameObject.Length; i++)
        {
            GameObject[i].SetActive(false);
        }
        GameObject[active].SetActive(true);
	}
	

}
