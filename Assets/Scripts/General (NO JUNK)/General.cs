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

    //todo Collsion detectie, Pellet verdwijning, Health decrease etc zijn client base ---> moet server based zijn of 1 client moet bepalen wat er gebruit
    // Probleem: bij een speler ziet hij de health van pacman verdwijnen terwijl een ander speler dit niet ziet vanwege delay --> sommige methodes worden ook 5x heroepen als er een pacman en 4 ghost zijn
    public void ResetBroadCast()
    { 
        BroadcastMessage("Reset");
    }
}
