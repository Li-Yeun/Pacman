using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour {

    [SerializeField] GameObject ChoosePacman, LockedPacman;
    [SerializeField] GameObject ChooseGhost, LockedGhost;
    void Start ()
    {
        CheckPacman();
        CheckGhost();
    }

    private void CheckGhost()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 4)
        {
            LockedGhost.SetActive(false);
            ChooseGhost.SetActive(true);
        }
        else
        {
            LockedGhost.SetActive(true);
            ChooseGhost.SetActive(false);
        }
    }

    private void CheckPacman()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            LockedPacman.SetActive(false);
            ChoosePacman.SetActive(true);
        }
        else
        {
            LockedPacman.SetActive(true);
            ChoosePacman.SetActive(false);  
        }
    }

    public void PacmanInstantiated()
    {
        CheckPacman();
    }

    public void GhostInstantiated()
    {
        CheckGhost();
    }
}
