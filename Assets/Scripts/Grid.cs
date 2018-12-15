using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid: MonoBehaviour {
    int lengte,breedte;
    public int TimesTeleporterCreated = 0;
    public GameObject block1;
    public GameObject pellet;
    public GameObject powerpill;
    public GameObject SlidingDoor;
    public GameObject Teleporter;
    public char [,] gamegridd;
    char b;

    // Use this for initialization
    void Start()
    {
        lengte = 23;
        breedte = 37;

        gamegridd = new char [,] 
        {
            { 'b','b','b','b','b','b','b','b','t','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','t','b','b','b','b','b','b','b','b' },
            { 'b','e','e','e','e','e','e','e','e','b','p','e','e','e','e','e','e','e','b','e','e','e','e','e','e','e','p','b','e','e','e','e','e','e','e','e','b' },
            { 'b','e','b','b','b','e','b','b','e','b','e','b','b','e','b','b','b','e','b','e','b','b','b','e','b','b','e','b','e','b','b','e','b','b','b','e','b' },
            { 'b','e','b','b','e','e','b','b','e','b','e','b','b','e','b','b','b','e','b','e','b','b','b','e','b','b','e','b','e','b','b','e','e','b','b','e','b' },
            { 'b','e','b','b','e','b','b','b','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','b','b','b','e','b','b','e','b' },
            { 'b','e','e','e','e','b','b','b','e','b','e','b','b','e','b','e','b','b','b','b','b','e','b','e','b','b','e','b','e','b','b','b','e','e','e','e','b' },
            { 'b','e','b','b','e','e','e','e','e','b','e','b','b','e','b','e','e','e','b','e','e','e','b','e','b','b','e','b','e','e','e','e','e','b','b','e','b' },
            { 't','e','e','b','e','b','b','b','e','b','e','e','e','e','b','e','b','e','b','e','b','e','b','e','e','e','e','b','e','b','b','b','e','b','e','e','t' },
            { 'b','b','e','b','e','e','e','e','e','b','b','e','b','e','b','e','b','e','e','e','b','e','b','e','b','e','b','b','e','e','e','e','e','b','e','b','b' },
            { 'b','b','e','b','b','f','b','b','b','b','e','e','b','e','b','e','b','b','f','b','b','e','b','e','b','e','e','b','b','b','b','f','b','b','e','b','b' },
            { 'b','e','e','e','e','e','e','e','e','b','e','b','b','e','b','e','b','s','s','s','b','e','b','e','b','b','e','b','e','e','e','e','e','e','e','e','b' },
            { 'b','p','b','b','b','b','b','b','e','e','e','e','e','e','b','e','b','s','s','s','b','e','e','e','e','e','e','e','e','b','b','b','b','b','b','p','b' },
            { 'b','e','e','e','e','e','e','e','e','b','e','b','b','e','b','e','b','b','b','b','b','e','b','e','b','b','e','b','e','e','e','e','e','e','e','e','b' },
            { 'b','b','e','b','b','f','b','b','b','b','e','e','b','e','b','e','e','e','e','e','e','e','b','e','b','e','e','b','b','b','b','f','b','b','e','b','b' },
            { 'b','b','e','b','e','e','e','e','e','b','b','e','b','e','b','e','b','b','b','b','b','e','b','e','b','e','b','b','e','e','e','e','e','b','e','b','b' },
            { 't','e','e','b','e','b','e','b','e','b','e','e','e','e','e','e','e','e','b','e','e','e','e','e','e','e','e','b','e','b','e','b','e','b','e','e','t' },
            { 'b','e','b','b','e','b','e','b','e','b','e','b','b','f','b','b','b','e','b','e','b','b','b','f','b','b','e','b','e','b','e','b','e','b','b','e','b' },
            { 'b','e','e','e','e','b','e','b','e','b','e','e','b','e','e','e','e','e','e','e','e','e','e','e','b','e','e','b','e','b','e','b','e','e','e','e','b' },
            { 'b','e','b','b','b','b','e','e','e','b','b','e','b','e','b','e','b','b','b','b','b','e','b','e','b','e','b','b','e','e','e','b','b','b','b','e','b' },
            { 'b','e','b','e','e','e','e','b','e','e','e','e','e','e','b','e','e','e','b','e','e','e','b','e','e','e','e','b','e','b','e','e','e','e','b','e','b' },
            { 'b','e','b','e','b','b','b','b','e','b','e','b','b','b','b','b','b','e','b','e','b','b','b','b','b','b','e','b','e','b','b','b','b','e','b','e','b' },
            { 'b','e','e','e','e','e','e','e','e','b','p','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','p','b','e','e','e','e','e','e','e','e','b' },
            { 'b','b','b','b','b','b','b','b','t','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','t','b','b','b','b','b','b','b','b' },
        };
        SpawnGrid();
    }

    // Update is called once per frame
    void Update()
    {
            
    }
    void SpawnGrid()
    {
        for (int z = 0; z < breedte; z++)
            for (int x = 0; x < lengte; x++)
            {
                LoadBlock(gamegridd[x,z], z, x);
            } 
    }
    void LoadBlock(char TileType, int x, int z)
    {
        switch (TileType)
        {
            case 'b': 
                {
                    GameObject block = Instantiate(block1, Vector3.zero, block1.transform.rotation) as GameObject;
                    block.transform.parent = transform;
                    block.transform.localPosition = new Vector3(x, 1, z);
                    block.SetActive(true);
                }
                break;
            case 'e':
                {
                    GameObject pell = Instantiate(pellet, Vector3.zero, pellet.transform.rotation) as GameObject;
                    pell.transform.parent = transform;
                    pell.transform.localPosition = new Vector3(x, 1, z);
                    pell.SetActive(true);
                }
                break;
            case 'p':
                {
                    GameObject power = Instantiate(powerpill, Vector3.zero, powerpill.transform.rotation) as GameObject;
                    power.transform.parent = transform;
                    power.transform.localPosition = new Vector3(x, 1, z);
                    power.SetActive(true);
                }
                break;
            case 'f':
                {
                    GameObject door = Instantiate(SlidingDoor, Vector3.zero, SlidingDoor.transform.rotation) as GameObject;
                    door.transform.parent = transform;
                    door.transform.localPosition = new Vector3(x, 1, z);
                    door.SetActive(true);
                }
                break;
            case 't':
                {
                    TimesTeleporterCreated++;
                    GameObject Teleporterr = Instantiate(Teleporter, Vector3.zero, SlidingDoor.transform.rotation) as GameObject;
                    Teleporterr.transform.parent = transform;
                    Teleporterr.transform.localPosition = new Vector3(x, 1, z);
                    TeleportScript2 TeleporterScript = Teleporterr.GetComponent<TeleportScript2>();
                    switch (TimesTeleporterCreated)
                    {
                        default: TeleporterScript.code = 0; break;
                        case 1: TeleporterScript.code = 1; break;
                        case 2: TeleporterScript.code = 0; break;
                        case 3: TeleporterScript.code = 3; break;
                        case 4: TeleporterScript.code = 3; break;
                        case 5: TeleporterScript.code = 2; break;
                        case 6: TeleporterScript.code = 2; break;
                        case 7: TeleporterScript.code = 1; break;
                    }
                    Teleporter.SetActive(true);
                }
                break;
            default: break;
        }
    }
}
