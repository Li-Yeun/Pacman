using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectateCamera : MonoBehaviour {

    [SerializeField] GameObject PacmanCamera, GhostCamera;
    [SerializeField] GameObject DirectionLight;
    
	// Use this for initialization
	void Start () {
        gameObject.transform.parent = GameObject.Find("EveryObject").transform;
    }
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.P))
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
            {
                PacmanCamera.SetActive(!PacmanCamera.activeSelf);
                GhostCamera.SetActive(!GhostCamera.activeSelf);
                DirectionLight.SetActive(!DirectionLight.activeSelf);
            }
        }
	}
}
