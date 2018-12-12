using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public enum View { TopDown, FirstPerson }
    [SerializeField] GameObject NormalCamera, FirstPerson, MiniMap;
    [SerializeField] View view;
    private GameObject Player;

    private void Start()
    {
        Player = FindObjectOfType<PacmanMovement>().gameObject;
    }
    void Update () {

        if (Input.GetKeyDown(KeyCode.P))
        {
            switch(view)
            {
                case View.TopDown:
                    view = View.FirstPerson;
                    break;
                case View.FirstPerson:
                    view = View.TopDown;
                    break;
            }
        }

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
