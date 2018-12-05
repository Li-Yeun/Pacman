using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    public Button play;
    public Button about;
    public Button exitgame;
    public string gamescene;
    public GameObject About;
	
    public void PlayGame()
    {
        SceneManager.LoadScene(gamescene);
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
