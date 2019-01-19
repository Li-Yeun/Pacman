using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PelletCounter : MonoBehaviour {

    [SerializeField] int counter;
    private Text pelletCounter;
    private int resetCounter;
    public GameObject pacmanwins;

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
            pacmanwins.SetActive(true);
           // PlayerOnline Player = FindObjectOfType<PlayerOnline>();
            //Player.CmdReset();
            
        }
    }

    public void Reset()
    {
        counter = resetCounter;
    }
}
