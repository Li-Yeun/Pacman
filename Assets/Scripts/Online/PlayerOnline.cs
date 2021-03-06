﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/// <summary>
/// Spawns the player when a player joins the game.
/// </summary>
public class PlayerOnline : NetworkBehaviour
{

    [SerializeField] GameObject[] GhostObject;
    [SerializeField] GameObject PacmanObject, Spectator;
    [SerializeField] GameObject FirstPerson, MiniMap, TopDownCamera;
    [SerializeField] GameObject MiniMapLight, GeneralLight, PacmanBodyLight;
    [SerializeField] GameObject Decoy, Decoy_Camera;
    [SerializeField] GameObject HUD;
    private General BroadCaster;
    private Grid OriginalGrid;
    public bool SpawnDecoyBool = false;
    public List<Gridbased> griddbased, originalgrid;
 

    // Use this for initialization
    void Start()
    {
        NetworkManagerHUD hud = FindObjectOfType<NetworkManagerHUD>();

        if (hud != null)
            hud.showGUI = false;
        BroadCaster = FindObjectOfType<General>();

        if (isLocalPlayer)
        {
            FindObjectOfType<HUD>().ChooseCharacter.SetActive(true);
            griddbased = new List<Gridbased>();
            
        }
    }

    void Update()
    {
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.R))
        {
            if (this.gameObject.name == "Player Pacman")
            {
                CmdReset();
            }
        }

        if (isLocalPlayer && (SpawnDecoyBool == true))
        {
            if (GameObject.Find("Decoy(Clone)") != null)
                return;
            if (this.gameObject.name == "Player Pacman")
            {
                SpawnDecoy();
            }
        }
    }

    public void SpawnPacman()
    {
        if (!isLocalPlayer)
            return;
        CmdSpawnMyPacman();
        NormalSpawnMyPacman();
    }

    public void SpawnGhost(int number)
    {
        if (!isLocalPlayer)
            return;
        CmdSpawnMyGhost(number);
        NormalSpawnMyGhost();
    }

    public void Spectate()
    {
        if (!isLocalPlayer)
            return;
        Instantiate(Spectator);
        FindObjectOfType<HUD>().GeneralHUD.SetActive(true);
    }

    private void SpawnDecoy()
    {
        SpawnDecoyBool = false;
        CmdSpawnDecoy();
        //Instantiate(Decoy_Camera);
    }

    [Command]
    private void CmdSpawnDecoy()
    {
        GameObject go = Instantiate(Decoy);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

    [Command]
    private void CmdSpawnMyPacman()
    {
        GameObject Pacman = Instantiate(PacmanObject);
        NetworkServer.SpawnWithClientAuthority(Pacman, connectionToClient);
        RpcChangePacmanName();
        RpcSpawnMyPacman(Pacman);

    }
    [ClientRpc]
    private void RpcSpawnMyPacman(GameObject Object)
    {
        if(isLocalPlayer && this.gameObject.name == "Player Pacman")
        {
            Instantiate(PacmanBodyLight, Object.transform);
        }
    }

    private void NormalSpawnMyPacman()
    {
        FindObjectOfType<HUD>().PacmanHUD.SetActive(true);
        FindObjectOfType<HUD>().GeneralHUD.SetActive(true);
        Instantiate(FirstPerson);
        Instantiate(MiniMap);
        Instantiate(MiniMapLight);
        Instantiate(GeneralLight);
        OriginalGrid = FindObjectOfType<Grid>();
        RepeatSpawn();
    }

    [Command]
    private void CmdSpawnMyGhost(int number)
    {
        GameObject Ghost = Instantiate(GhostObject[number]);
        NetworkServer.SpawnWithClientAuthority(Ghost, connectionToClient);
        RpcChangeGhostName(number);
    }

    private void NormalSpawnMyGhost()
    {
        FindObjectOfType<HUD>().GhostHUD.SetActive(true);
        FindObjectOfType<HUD>().GeneralHUD.SetActive(true);
        Instantiate(TopDownCamera);
    }

    [ClientRpc]
    private void RpcChangePacmanName()
    {
        transform.name = "Player Pacman";
    }

    //todo herschrijven
    [ClientRpc]
    private void RpcChangeGhostName(int number)
    {
        switch (number)
        {
            case 1:
                transform.name = "Red Ghost";
                break;
            case 2:
                transform.name = "Blue Ghost";
                break;
            case 3:
                transform.name = "Orange Ghost";
                break;
            case 4:
                transform.name = "Pink Ghost";
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
    private void RpcReset()
    {
        if (isLocalPlayer && this.gameObject.name == "Player Pacman")
        {
            griddbased = new List<Gridbased>();
            Save();
        }
        FindObjectOfType<PelletCounter>().pacmanwins.SetActive(false);
        FindObjectOfType<playerhealth>().ghostwins.SetActive(false);
        BroadCaster.ResetBroadCast();
    }

    private void RepeatSpawn()
    {
        Save();
        InvokeRepeating("SpawnRandomFruit", Random.Range(10f, 15f), Random.Range(10f, 15f));// Spawns the fruit every 10-15 seconds somewhere on the map (indicated with an s)
    }

    private void Save()
    {
        for (int z = 0; z < OriginalGrid.gamegrid.GetLongLength(1); z++)
            for (int x = 0; x < OriginalGrid.gamegrid.GetLongLength(0); x++)
            {
                if (OriginalGrid.gamegrid[x, z] == 's')
                {
                    griddbased.Add(new Gridbased(z, x));
                }
            }
    }
    private void SpawnRandomFruit()
    {
        if (griddbased.Count == 0) { return; }
        Gridbased gridbased = griddbased[Random.Range(0, griddbased.Count)];
        switch (Random.Range(0,5))
        {
            case 0: LoadBlock('w', gridbased.x, gridbased.z); break;
            case 1: LoadBlock('a', gridbased.x, gridbased.z); break;
            case 2: LoadBlock('o', gridbased.x, gridbased.z); break;
            case 3: LoadBlock('m', gridbased.x, gridbased.z); break;
            case 4: LoadBlock('k', gridbased.x, gridbased.z); break;
        }
        griddbased.Remove(gridbased);
    }

    private void LoadBlock(char TileType, int x, int z)
    {
        switch (TileType)
        {
            case 'w':
                {
                    CmdInstantiateObject(OriginalGrid.Aardbei, x, z, OriginalGrid.AardbeiParent.gameObject);
                }
                break;
            case 'a':
                {
                    CmdInstantiateObject(OriginalGrid.Appel, x, z, OriginalGrid.AppelParent.gameObject);
                }
                break;
            case 'o':
                {
                    CmdInstantiateObject(OriginalGrid.Sinaasappel, x, z, OriginalGrid.SinaasappelParent.gameObject);
                }
                break;
            case 'm':
                {
                    CmdInstantiateObject(OriginalGrid.Meloen, x, z, OriginalGrid.MeloenParent.gameObject);
                }
                break;
            case 'k':
                {
                    CmdInstantiateObject(OriginalGrid.Kers, x, z, OriginalGrid.KersParent.gameObject);
                }
                break;
            default: break;
        }
    }
    [Command]
    private void CmdInstantiateObject(GameObject gameObjecttt, int x, int z, GameObject Parent)
    {
        GameObject gameObjectt = Instantiate(gameObjecttt, Vector3.zero, gameObjecttt.transform.rotation);
        gameObjectt.transform.parent = Parent.transform;
        gameObjectt.transform.localPosition = new Vector3(x, 1, z);
        NetworkServer.SpawnWithClientAuthority(gameObjectt, connectionToClient);
    }

    public void AddToGridList(int x, int y)
    {
        if(isLocalPlayer && this.gameObject.name == "Player Pacman")
            griddbased.Add(new Gridbased(x,y));
    }
}

public class Gridbased
{
    public int x;
    public int z;
    public Gridbased(int _x,int _z)
    {
        x = _x;
        z = _z;
    }
}