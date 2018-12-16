using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitroenPowerup : MonoBehaviour {

    public GameObject Citroen;
    public PacmanMovement pacmanMovement;
    // Use this for initialization
    void Start()
    {
        Citroen = this.gameObject;
        pacmanMovement = FindObjectOfType<PacmanMovement>();
    }
    void OnTriggerStay(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Pellet":
                Citroen.SetActive(false);
                break;
            case "Player":
                Citroen.SetActive(false);
                pacmanMovement.Speed.x++;
                pacmanMovement.Speed.z++;

                break;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
