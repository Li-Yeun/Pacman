using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class SpawnFruit : NetworkBehaviour {

    private Grid OriginalGrid;
	// Use this for initialization
	void Start () {

    }

    /*
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
    */
}
