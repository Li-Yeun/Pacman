using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour {
    public void Broadcast()
    {
        BroadcastMessage("PacmanInstantiated");
    }
}
