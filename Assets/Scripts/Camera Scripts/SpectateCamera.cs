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

		if(Input.GetKeyDown("1"))           // Wanneer je op 1 drukt als spectator wordt je camera perspectief verander naar de perspectief van pacman of het geestje
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length == 1)        //Checken of pacman wel bestaat voordat de camera naar het perspectief van pacman verandert
            {
                PacmanCamera.SetActive(!PacmanCamera.activeSelf);
                GhostCamera.SetActive(!GhostCamera.activeSelf);
                DirectionLight.SetActive(!DirectionLight.activeSelf);
            }
        }
    }
}
