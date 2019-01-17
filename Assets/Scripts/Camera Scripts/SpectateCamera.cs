using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectateCamera : MonoBehaviour {

    [SerializeField] GameObject PacmanCamera, GhostCamera;
    [SerializeField] GameObject DirectionLight;
    private GameObject UI;
    
	// Use this for initialization
	void Start () {
        gameObject.transform.parent = GameObject.Find("EveryObject").transform;
        UI = GameObject.Find("HUD General");
    }
	
	void Update () {
		if(Input.GetKeyDown("1"))
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
            {
                PacmanCamera.SetActive(!PacmanCamera.activeSelf);
                GhostCamera.SetActive(!GhostCamera.activeSelf);
                DirectionLight.SetActive(!DirectionLight.activeSelf);
            }
        }

        if (Input.GetKeyDown("2"))
        {
            UI.SetActive(!UI.activeSelf);
        }
    }
}
