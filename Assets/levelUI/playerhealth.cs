using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerhealth : MonoBehaviour {

    public int health = 2;
    public Texture2D Health3;
    public Texture2D Health2;
    public Texture2D Health1;
    public Texture2D currentHealth;
    Rect rec;

    private void Start()
    {
        rec.width = Screen.width - 100;
        rec.height = Screen.height - 50;

    }
    private void OnGUI()
    {
        GUI.DrawTexture(rec, currentHealth, ScaleMode.ScaleToFit);
    }
    void Update () {
        if (health <= 0)
        { Dead(); }
        Healthbar();
	}


    void Dead()
    { }

    public void Healthbar()
    {
        if (health == 3)
        { currentHealth = Health3; }
            else if (health == 2)
            { currentHealth = Health2; }
                else if (health == 1)
                    { currentHealth = Health1; }
    }
}
