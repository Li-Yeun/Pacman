using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class playerhealth : MonoBehaviour {

    public int health;
    public Image[] health_img;
    public Sprite pacman_healthicon;
    public GameObject ghostwins;

    void Update () {
        //check if player is dead
        if (health <= 0)        
        {
            Dead();
        }

        //checks the health and enables the images for display
        for (int i = 0; i < health_img.Length; i++)     
        {
            if (i < health) { 
                health_img[i].enabled = true;
            health_img[i].sprite = pacman_healthicon; }
            else { health_img[i].enabled = false; }
        }
	}

    void Dead()
    {
        ghostwins.SetActive(true);
        //PlayerOnline Player = FindObjectOfType<PlayerOnline>();
        //Player.CmdReset();
    }


    public void DecreaseHealth()
    {
        health--;
    }

    public void IncreaseHealth()
    {
        health++;
    }

    public void Reset()
    {
        health = 3;
    }
}
