using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour {

    [SerializeField] GameObject Spectator;
    [SerializeField] GameObject[] ChooseGhost;
    [SerializeField] GameObject ChoosePacman, LockedPacman, LockedGhost;
    void Start ()
    {
        Spectator.SetActive(true);
        CheckPacman();
        CheckGhost();
    }

    private void CheckGhost()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length >= 4)
        {
            LockedGhost.SetActive(true);
            foreach (GameObject Ghost in ChooseGhost)
            {
                Ghost.SetActive(false);
            }
        } else
        {
            LockedGhost.SetActive(false);
            for(int i = 0; i< ChooseGhost.Length; i++)
            {
                if(i == GameObject.FindGameObjectsWithTag("Enemy").Length)
                {
                    ChooseGhost[i].SetActive(true);
                }
                else
                {
                    ChooseGhost[i].SetActive(false);
                }
            }
            
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
