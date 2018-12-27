using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerOnline : NetworkBehaviour {

    [SerializeField] GameObject GhostObject;
    [SerializeField] GameObject PacmanObject;
    [SerializeField] GameObject FirstPerson, MiniMap, TopDownCamera;
    [SerializeField] GameObject MiniMapLight, GeneralLight;

    // Use this for initialization
    void Start () {

        if (isLocalPlayer)
        {
            if (FindObjectsOfType<PacmanMovement>().Length == 0)
            {
                CmdSpawnMyPacman();
                NormalSpawnMyPacman();

            }
            else
            {
                CmdSpawnMyGhost();
                NormalSpawnMyGhost();
            }
        }
    }

    [Command]
    void CmdSpawnMyPacman()
    {
        GameObject Pacman = Instantiate(PacmanObject);
        NetworkServer.SpawnWithClientAuthority(Pacman, connectionToClient);

    }

    void NormalSpawnMyPacman()
    {
        Instantiate(FirstPerson);
        Instantiate(MiniMap);
        Instantiate(MiniMapLight);
        Instantiate(GeneralLight);
    }

    [Command]
    void CmdSpawnMyGhost()
    {
        GameObject Ghost = Instantiate(GhostObject);
        NetworkServer.SpawnWithClientAuthority(Ghost, connectionToClient);
    }

    void NormalSpawnMyGhost()
    {
      Instantiate(TopDownCamera);
    }

}
