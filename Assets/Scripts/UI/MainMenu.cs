using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    public Button loadplay, play;
    public Button multiplayer;
    public Button about;
    public Button exitgame;
    public string gamescene, multiplayerscene;
    public GameObject About, buttonexplain, black;

    
    public void startgame()
    { SceneManager.LoadScene(gamescene);
        black.SetActive(true); }

    //loads multiplayer scenario
    public void Multiplayer()
    { SceneManager.LoadScene(multiplayerscene); }

    //enables button explanation popupscreen
    public void Buttonexplain()
    {
        buttonexplain.SetActive(true);  
    }

    //enables about menu popupscreen
    public void AboutGame()
    {
        About.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
