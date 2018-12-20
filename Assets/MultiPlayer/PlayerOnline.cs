using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerOnline : NetworkBehaviour {

    [SerializeField] GameObject GhostObject;
    [SerializeField] GameObject PacmanObject;
    [SerializeField] GameObject FirstPerson, MiniMap, TopDownCamera;
    // Use this for initialization
    void Start () {
        if (!isLocalPlayer)
        {
            FirstPerson.SetActive(false);
            MiniMap.SetActive(false);
            TopDownCamera.SetActive(false);
        }
        else if (isLocalPlayer)
        {
            if (FindObjectsOfType<PacmanMovement>().Length == 0)
            {
                CmdSpawnMyPacman();
                FirstPerson.SetActive(true);
                MiniMap.SetActive(true);
                TopDownCamera.SetActive(false);
            }
            else if (FindObjectsOfType<PacmanMovement>().Length >= 1)
            {
                CmdSpawnMyGhost();

                // Dit werkt nog niet
                TopDownCamera.SetActive(true);
                FirstPerson.SetActive(false);
                MiniMap.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update () {
        return;
	}

    [Command]
    void CmdSpawnMyPacman()
    {
        GameObject Pacman = Instantiate(PacmanObject);

        GameObject firstPerson = Instantiate(FirstPerson);
        GameObject miniMap = Instantiate(MiniMap);

        NetworkServer.SpawnWithClientAuthority(Pacman, connectionToClient);
        NetworkServer.SpawnWithClientAuthority(firstPerson, connectionToClient);
        NetworkServer.SpawnWithClientAuthority(miniMap, connectionToClient);
    }

    [Command]
    void CmdSpawnMyGhost()
    {
        GameObject Ghost = Instantiate(GhostObject);
        GameObject topDownCamera = Instantiate(TopDownCamera);
        NetworkServer.SpawnWithClientAuthority(Ghost, connectionToClient);
        NetworkServer.SpawnWithClientAuthority(TopDownCamera, connectionToClient);
    }

}
