using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSelection : MonoBehaviour {

    [SerializeField] GameObject RedGhost,BlueGhost,OrangeGhost,PinkGhost;
    [SerializeField] GameObject RedGhostLocked, BlueGhostLocked, OrangeGhostLocked, PinkGhostLocked;

    private Movement[] CurrentGhost;
    // Use this for initialization
    void Start () {

        CheckColorGhost();
    }

    private void CheckColorGhost()
    {
        CurrentGhost = FindObjectsOfType<Movement>();
        foreach (Movement ghost in CurrentGhost)
        {
            if (ghost == null)
                return;

            switch (ghost.name)
            {
                case "Red":
                    RedGhost.SetActive(false);
                    RedGhostLocked.SetActive(true);
                    break;
                case "Blue":
                    BlueGhost.SetActive(false);
                    BlueGhostLocked.SetActive(true);
                    break;
                case "Orange":
                    OrangeGhost.SetActive(false);
                    OrangeGhostLocked.SetActive(true);
                    break;
                case "Pink":
                    PinkGhost.SetActive(false);
                    PinkGhostLocked.SetActive(true);
                    break;
            }
        }
    }

    public void GhostInstantiated()
    {
        CheckColorGhost();
    }

}
