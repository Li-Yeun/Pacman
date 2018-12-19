using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour {

    public Button continu;
    public Button mainmenu;
    public Button options;
    public Button quitgame;
    public Button pause;
    public string gamescene;
    public GameObject pausemenu;
    public GameObject optionmenu;

    public void Pause()
    {
        pausemenu.SetActive(true);
        
    }

    public void Mainmenu()
    {
        SceneManager.LoadScene(gamescene);
    }

    public void Options()
    {
        optionmenu.SetActive(true);
    }


    public void Quitgame()
    {
        Application.Quit();
    }


    public void Continu()
    {
        pausemenu.SetActive(false);
    }
}