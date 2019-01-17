using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PelletCounter : MonoBehaviour {

    [SerializeField] int counter;
    private Text pelletCounter;
    private int resetCounter;

    // Use this for initialization
    void Start()
    {
        pelletCounter = GetComponent<Text>();
        resetCounter = counter;
        pelletCounter.text = "X " + counter;
    }

    void Update()
    {
        pelletCounter.text = "X " + counter;
    }

    public void DecreaseCounter()
    {
        counter--;
        pelletCounter.text = "X " + counter;
        if (counter <= 0)
        {
            //TODO winscreen
            PlayerOnline Player = FindObjectOfType<PlayerOnline>();
            Player.CmdReset();
            Debug.Log("PACMAN WINS!");
        }
    }

    public void Reset()
    {
        counter = resetCounter;
    }
}
