using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    [SerializeField] GameObject NormalCamera, FirstPerson, MiniMap, EnemyCamera;
    public GameObject Player;
    public enum View {TopDown, FirstPerson }
    public View view;
	void Update () {

        switch (view)
        {
            case View.FirstPerson:
                NormalCamera.SetActive(false);
                FirstPerson.SetActive(true);
                MiniMap.SetActive(true);
                Player.GetComponent<PacmanMovement>().SwitchControls = false; 
                break;

            case View.TopDown:
                
                    NormalCamera.SetActive(true);
                    FirstPerson.SetActive(false);
                    MiniMap.SetActive(false);
                    Player.GetComponent<PacmanMovement>().SwitchControls = true;
                break;
            default:
                view = View.TopDown;
                break;
            
        }
    }
}
