using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {
    [SerializeField] float time;

	void Start () {
        FindObjectOfType<EnvironementalEvents>().ResetTimer();
        Destroy(gameObject, time);
	}
}
