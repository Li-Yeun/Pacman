using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour {
    public void PacmanBroadcast()
    {
        BroadcastMessage("PacmanInstantiated");
    }

    public void GhostBroadcast()
    {
        BroadcastMessage("GhostInstantiated");
    }
}
