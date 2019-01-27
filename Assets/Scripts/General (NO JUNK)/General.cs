using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour
{
    public void PacmanBroadcast()
    {
        if(FindObjectOfType<CharacterSelection>() != null)
        BroadcastMessage("PacmanInstantiated");
    }

    public void GhostBroadcast()
    {
        BroadcastMessage("GhostInstantiated");
    }

    public void ResetBroadCast()
    { 
        BroadcastMessage("Reset");
    }
}
