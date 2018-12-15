using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        { Dead(); }
        Healthbar();
	}

    void Dead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Healthbar()
    {
        if (health == 3)
        { currentHealth = Health3; }
            else if (health == 2)
            { currentHealth = Health2; }
                else if (health == 1)
                    { currentHealth = Health1; }
    }

    public void DecreaseHealth()
    {
        health--;
    }
}
