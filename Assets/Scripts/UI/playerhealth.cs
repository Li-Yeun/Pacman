using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class playerhealth : MonoBehaviour {

    public int health = 3;
    public Texture2D Health3;
    public Texture2D Health2;
    public Texture2D Health1;
    public Texture2D currentHealth;
    Rect rec;

    private float lengte = 300, breedte = 300;
    private float xpos = 90, ypos = 850;

    private void Start()
    {
        rec.size = new Vector2(lengte, breedte);
        rec.position = new Vector2(xpos, ypos);
    }
    private void OnGUI()
    {
        GUI.DrawTexture(rec, currentHealth, ScaleMode.ScaleToFit);
    }
    void Update () {
        if (health <= 0)
        {
            Dead();
        }

        //Todo optimize
        Healthbar();
	}

    void Dead()
    {
        PlayerOnline Player = FindObjectOfType<PlayerOnline>();
        Player.CmdReset();
    }

    public void Healthbar()
    {
        switch(health)
        {
            case 3:
                currentHealth = Health3;
                break;
            case 2:
                currentHealth = Health2;
                break;
            case 1:
                currentHealth = Health1;
                break;
            default:
                currentHealth = Health3;
                health = 3;
                break;
        }
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
