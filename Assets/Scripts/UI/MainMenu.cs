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

    public AudioSource buttonclicksound;

    void Start()
    { buttonclicksound = GetComponent<AudioSource>(); }

    //starts game
    public void startgame()
    { SceneManager.LoadScene(gamescene);
         }

    //enables button explanation popupscreen
    public void Buttonexplain()
    {
        buttonexplain.SetActive(true);  
    }

    //quits game
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Buttonclick()
    { buttonclicksound.Play(); }
}
