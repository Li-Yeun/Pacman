using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    public Button loadplay, play;
    public Button exitgame;
    public string gamescene, multiplayerscene;
    public GameObject buttonexplain, black;

    
    public void startgame()
    { SceneManager.LoadScene(gamescene);
        black.SetActive(true); }

    //enables button explanation popupscreen
    public void Buttonexplain()
    {
        buttonexplain.SetActive(true);  
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
