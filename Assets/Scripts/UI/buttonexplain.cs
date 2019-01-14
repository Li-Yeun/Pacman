using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonexplain : MonoBehaviour {

    public GameObject Buttons, Powerups, Ability, ghostbackground;
    public Button arrowleft, arrowright;

    public void buttons()
    {
        Buttons.SetActive(true);
        Powerups.SetActive(false);
        Ability.SetActive(false);
        ghostbackground.SetActive(false);
    }

    public void powerups()
    {
        Buttons.SetActive(false);
        Powerups.SetActive(true);
        Ability.SetActive(false);
        ghostbackground.SetActive(false);
    }

    public void ability()
    {
        Buttons.SetActive(false);
        Powerups.SetActive(false);
        Ability.SetActive(true);
        ghostbackground.SetActive(true);
    }
}
