﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerOnline : NetworkBehaviour {

    [SerializeField] GameObject GhostObject;
    [SerializeField] GameObject PacmanObject;
    [SerializeField] GameObject FirstPerson, MiniMap, TopDownCamera;
    [SerializeField] GameObject MiniMapLight, GeneralLight;
    private General BroadCaster;


    // Use this for initialization
    void Start () {
        NetworkManagerHUD hud = FindObjectOfType<NetworkManagerHUD>();
        if (hud != null)
            hud.showGUI = false;
        BroadCaster = FindObjectOfType<General>();
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

    public void Update()
    {
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.R))
        {
            if (this.gameObject.name == "Player Pacman")
            {
                CmdReset();
            }
        }
    }
    [Command]
    void CmdSpawnMyPacman()
    {
        GameObject Pacman = Instantiate(PacmanObject);
        NetworkServer.SpawnWithClientAuthority(Pacman, connectionToClient);
        RpcChangePacmanName();

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
        RpcChangeGhostName();
    }

    void NormalSpawnMyGhost()
    {
      Instantiate(TopDownCamera);
    }

    [ClientRpc]
    void RpcChangePacmanName()
    {
        transform.name = "Player Pacman";
    }
    [ClientRpc]
    void RpcChangeGhostName()
    {
        int number = FindObjectsOfType<PlayerOnline>().Length - 1;
        switch (number)
        {
            case 1: transform.name = "Blue Ghost";
                break;
            case 2:
                transform.name = "Orange Ghost";
                break;
            case 3:
                transform.name = "Pink Ghost";
                break;
            case 4:
                transform.name = "Red Ghost";
                break;
            default:
                break;
        }
        
    }
    [Command]
    public void CmdReset()
    {
        {
            RpcReset();
        }
    }

    [ClientRpc]
    public void RpcReset()
    {
        BroadCaster.ResetBroadCast();
    }

}
