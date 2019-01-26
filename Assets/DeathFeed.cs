using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathFeed : MonoBehaviour {

    [SerializeField] float DisplayTime = 2f;
    string Object1, Object2;
    Text TextDisplay;

    private void Start()
    {
        TextDisplay = GetComponent<Text>();
    }
    void Update () {
        TextDisplay.text = Object1 + " killed " + Object2;
	}

    public void GhostKilledPacman(string GhostCollor)
    {
        Object1 = GhostCollor + " Ghost";
        Object2 = "Pacman";
        Invoke("TurnTextOff", DisplayTime);
    }
    void TurnTextOff()
    {
        gameObject.SetActive(false);
    }
}
