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
    public GameObject ChooseCharacter;

    //opens pausescreen
    public void Pause()
    {
        pausemenu.SetActive(true);
    }

    //go back to mainmenu
    public void Mainmenu()
    {
        SceneManager.LoadScene(gamescene);
    }

    //opens option menu
    public void Options()
    {
        optionmenu.SetActive(true);
    }
    
    public void Quitgame()
    {
        Application.Quit();
    }
    
    //disables menu 
    public void Continu()
    {
        pausemenu.SetActive(false);
    }

    public void ChoosePacman()
    {
        PlayerOnline Player= FindObjectOfType<PlayerOnline>();
        Player.SpawnPacman();
        ChooseCharacter.SetActive(false);

    }
    public void CHooseGhost()
    {
        PlayerOnline Player = FindObjectOfType<PlayerOnline>();
        Player.SpawnGhost();
        ChooseCharacter.SetActive(false);
    }
}