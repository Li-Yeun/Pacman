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
    public GameObject About, buttonexplain;


    public void startgame()
    { SceneManager.LoadScene(gamescene); }

    public void Multiplayer()
    { SceneManager.LoadScene(multiplayerscene); }

    public void Buttonexplain()
    {
        buttonexplain.SetActive(true);  
    }

    public void AboutGame()
    {
        About.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
