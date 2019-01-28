using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour
{
    // Deze methode wordt geroepen wanneer Pacman gespawned is, waarbij die vervolgens een broadcast naar iedereen stuurt dat er een geestjes gespawned is waarna ze vervolgens zichzelf ervoor moeten zorgen welke variable verandert moeten worden
    public void PacmanBroadcast()
    {
        if(FindObjectOfType<CharacterSelection>() != null)
        BroadcastMessage("PacmanInstantiated");
    }

    // Deze methode wordt geroepen wanneer er een geestje gespawned is, waarbij die vervolgens een broadcast naar iedereen stuurt dat er een geestjes gespawned is waarna ze vervolgens zichzelf ervoor moeten zorgen welke variable verandert moeten worden
    public void GhostBroadcast()
    {
        BroadcastMessage("GhostInstantiated");
    }


    // Deze methode zorgt ervoor dat alle reset methodes geroepen worden bij iedere script
    public void ResetBroadCast()
    { 
        BroadcastMessage("Reset");
    }
}
