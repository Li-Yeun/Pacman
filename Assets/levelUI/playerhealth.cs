using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerhealth : MonoBehaviour {

   
    int health = 3;
    public Texture2D Health3;
    public Texture2D Health2;
    public Texture2D Health1;
    public Texture2D currentHealth;

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
