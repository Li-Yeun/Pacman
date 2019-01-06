﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisingWater : MonoBehaviour {

    [SerializeField] Vector3 StartOffset;
    [SerializeField] float Speed;
    Vector3 defaulthPos;
	void Start () {
        defaulthPos = gameObject.transform.position;
        gameObject.transform.position -= StartOffset;
	}
	
	void Update () {
		if(gameObject.transform.position.y < defaulthPos.y)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + Speed * Time.deltaTime, gameObject.transform.position.z);
        }
        else
        {
            gameObject.transform.position = defaulthPos;
        }

	}
}