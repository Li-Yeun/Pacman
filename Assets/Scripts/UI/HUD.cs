using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
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
    public GameObject ChooseCharacter, Characters;
    public GameObject PacmanHUD;
    public GameObject GhostHUD;
    public GameObject PacmanError, GhostError,ColorGhostError;
    public GameObject AllGhost;
    //opens pausescreen
    public void Pause()
    {
        pausemenu.SetActive(true);
        NetworkManagerHUD hud = FindObjectOfType<NetworkManagerHUD>();
        if (hud != null)
            hud.showGUI = true;
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
        NetworkManagerHUD hud = FindObjectOfType<NetworkManagerHUD>();
        if (hud != null)
            hud.showGUI = false;
    }

    public void ChoosePacman()
    {
        PlayerOnline Player= FindObjectOfType<PlayerOnline>();
        Player.SpawnPacman();
        ChooseCharacter.SetActive(false);
        GhostError.SetActive(false);
    }
    public void PacmanLocked()
    {
        PacmanError.SetActive(true);
    }
    public void GhostLocked()
    {
        GhostError.SetActive(true);
    }
    public void ChooseGhost()
    {
        AllGhost.SetActive(true);
        Characters.SetActive(false);

    }

    public void RedGhost()
    {
        ChooseColorGhost(0);
    }
    public void BlueGhost()
    {
        ChooseColorGhost(1);
    }
    public void OrangeGhost()
    {
        ChooseColorGhost(2);
    }
    public void PinkGhost()
    {
        ChooseColorGhost(3);
    }

    public void ChooseColorGhost(int number)
    {
        PlayerOnline Player = FindObjectOfType<PlayerOnline>();
        Player.SpawnGhost(number);
        ChooseCharacter.SetActive(false);
        PacmanError.SetActive(false);
        ColorGhostError.SetActive(false);
    }

    public void ColorGhostErrorMessage()
    {
        ColorGhostError.SetActive(true);
    }
}