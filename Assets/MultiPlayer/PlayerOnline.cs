using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerOnline : NetworkBehaviour
{

    [SerializeField] GameObject[] GhostObject;
    [SerializeField] GameObject PacmanObject, Spectator;
    [SerializeField] GameObject FirstPerson, MiniMap, TopDownCamera;
    [SerializeField] GameObject MiniMapLight, GeneralLight;
    [SerializeField] GameObject Decoy, Decoy_Camera;
    private General BroadCaster;
    private Grid OriginalGrid;


    // Use this for initialization
    void Start()
    {
       // gameObject.transform.parent = GameObject.Find("EveryObject").transform;
        NetworkManagerHUD hud = FindObjectOfType<NetworkManagerHUD>();

        if (hud != null)
            hud.showGUI = false;
        BroadCaster = FindObjectOfType<General>();
        if (isLocalPlayer)
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length == 1 && GameObject.FindGameObjectsWithTag("Enemy").Length == 4)
            {
                Spectate();
            }
            else
                FindObjectOfType<HUD>().ChooseCharacter.SetActive(true);
            /*
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
          */
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
        Instantiate(Spectator);
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

        if (isLocalPlayer && Input.GetKeyDown("8"))
        {
            if (this.gameObject.name == "Player Pacman")
            {
                SpawnDecoy();
            }
        }

    }

    public void SpawnDecoy()
    {
        CmdSpawnDecoy();
        Instantiate(Decoy_Camera);
    }

    [Command]
    public void CmdSpawnDecoy()
    {
        GameObject go = Instantiate(Decoy);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
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
    void CmdSpawnMyGhost(int number)
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 4)
        {
            Debug.Log("Max Ghost");
            Spectate();
            return;
        }
        GameObject Ghost = Instantiate(GhostObject[number]);
        NetworkServer.SpawnWithClientAuthority(Ghost, connectionToClient);
        RpcChangeGhostName();
    }

    void NormalSpawnMyGhost()
    {
        FindObjectOfType<HUD>().GhostHUD.SetActive(true);
        FindObjectOfType<HUD>().GeneralHUD.SetActive(true);
        Instantiate(TopDownCamera);
    }

    [ClientRpc]
    void RpcChangePacmanName()
    {
        transform.name = "Player Pacman";
    }

    //todo herschrijven
    [ClientRpc]
    void RpcChangeGhostName()
    {
        int number = FindObjectsOfType<PlayerOnline>().Length - 1;
        switch (number)
        {
            case 1:
                transform.name = "Blue Ghost";
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

    public void RepeatSpawn()
    {
        InvokeRepeating("SpawnRandomFruit", Random.Range(0, 4f), Random.Range(0, 4f));
    }

    bool Spawned = true;
    private void SpawnRandomFruit()
    {
        Spawned = false;
        for (int z = 0; z < OriginalGrid.gamegrid.GetLongLength(1); z++)
            for (int x = 0; x < OriginalGrid.gamegrid.GetLongLength(0); x++)
            {
                Check(OriginalGrid.gamegrid[x, z], z, x);
                if (Spawned)
                {
                    return;
                }
            }
    }
    private void Check(char TileType, int x, int z)
    {
        if (TileType == 's')
        {
            switch ((int)Random.Range(0, 5))
            {
                case 0: LoadBlock('w', x, z); break;
                case 1: LoadBlock('a', x, z); break;
                case 2: LoadBlock('o', x, z); break;
                case 3: LoadBlock('m', x, z); break;
                case 4: LoadBlock('k', x, z); break;
            }
            OriginalGrid.gamegrid[z, x] = 'd';
            Spawned = true;
        }
    }

    public void LoadBlock(char TileType, int x, int z)
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
    public void CmdInstantiateObject(GameObject gameObjecttt, int x, int z, GameObject Parent)
    {
        GameObject gameObjectt = Instantiate(gameObjecttt, Vector3.zero, gameObjecttt.transform.rotation);
        gameObjectt.transform.parent = Parent.transform;
        gameObjectt.transform.localPosition = new Vector3(x, 1, z);
        NetworkServer.SpawnWithClientAuthority(gameObjectt, connectionToClient);
    }
}
