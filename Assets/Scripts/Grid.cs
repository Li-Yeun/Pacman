using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Deze class is verantwoordelijk voor het initialise van het level.
/// </summary>
public class Grid: MonoBehaviour {
    int lengte,breedte;
    public int TimesTeleporterCreated = 0;
    float Timer = 0;
    public GameObject block1;
    public GameObject pellet;
    public GameObject powerpill;
    public GameObject SlidingDoor;
    public GameObject Teleporter;
    public GameObject SpawnPacman;
    public GameObject Citroen;
    public char [,] gamegridd;
    char b;

    void Start()
    {
        lengte = 23;
        breedte = 37;
        // Dit is de level layout, hier staat in waar wat wordt geplaatst in de grid op het moment dat het level geladen wordt.
        gamegridd = new char [,] 
        {
            { 'b','b','b','b','b','b','b','b','t','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','t','b','b','b','b','b','b','b','b' },
            { 'b','e','e','e','e','e','e','b','e','b','p','e','e','e','e','e','e','e','b','e','e','e','e','e','e','e','p','b','e','b','e','e','e','e','e','e','b' },
            { 'b','e','b','b','b','b','e','e','e','b','e','b','b','e','b','b','b','e','b','e','b','b','b','e','b','b','e','b','e','e','e','b','b','b','b','e','b' },
            { 'b','e','b','b','e','e','e','b','e','b','e','b','b','e','b','b','b','e','b','e','b','b','b','e','b','b','e','b','e','b','e','e','e','b','b','e','b' },
            { 'b','e','b','b','e','b','b','b','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','b','b','b','e','b','b','e','b' },
            { 'b','e','e','e','e','b','b','b','e','b','e','b','b','e','b','e','b','b','b','b','b','e','b','e','b','b','e','b','e','b','b','b','e','e','e','e','b' },
            { 'b','b','e','b','e','e','e','e','e','b','e','b','b','e','b','e','e','e','b','e','e','e','b','e','b','b','e','b','e','e','e','e','e','b','e','b','b' },
            { 't','e','e','b','e','b','b','b','e','b','e','e','e','e','b','e','b','e','b','e','b','e','b','e','e','e','e','b','e','b','b','b','e','b','e','e','t' },
            { 'b','b','e','b','e','e','e','e','e','b','b','e','b','e','b','e','b','e','e','e','b','e','b','e','b','e','b','b','e','e','e','e','e','b','e','b','b' },
            { 'b','b','e','b','b','f','b','b','b','b','e','e','b','e','b','e','b','b','f','b','b','e','b','e','b','e','e','b','b','b','b','f','b','b','e','b','b' },
            { 'b','e','e','e','e','e','e','e','e','b','e','b','b','e','b','e','b','s','s','s','b','e','b','e','b','b','e','b','e','e','e','e','e','e','e','e','b' },
            { 'b','p','b','b','b','b','b','b','e','e','e','e','e','e','b','e','b','s','s','s','b','e','e','e','e','e','e','e','e','b','b','b','b','b','b','p','b' },
            { 'b','e','e','e','e','e','e','e','e','b','e','b','b','e','b','e','b','b','b','b','b','e','b','e','b','b','e','b','e','e','e','e','e','e','e','e','b' },
            { 'b','b','e','b','b','f','b','b','b','b','e','e','b','e','b','e','e','e','e','e','e','e','b','e','b','e','e','b','b','b','b','f','b','b','e','b','b' },
            { 'b','b','e','b','e','e','e','e','e','b','b','e','b','e','b','e','b','b','b','b','b','e','b','e','b','e','b','b','e','e','e','e','e','b','e','b','b' },
            { 't','e','e','b','e','b','e','b','e','b','e','e','e','e','e','e','e','e','b','e','e','e','e','e','e','e','e','b','e','b','e','b','e','b','e','e','t' },
            { 'b','b','e','b','e','b','e','b','e','b','e','b','b','f','b','b','b','e','b','e','b','b','b','f','b','b','e','b','e','b','e','b','e','b','e','b','b' },
            { 'b','e','e','e','e','b','e','b','e','b','e','e','b','e','e','e','e','e','k','e','e','e','e','e','b','e','e','b','e','b','e','b','e','e','e','e','b' },
            { 'b','e','b','b','b','b','e','b','e','b','b','e','b','e','b','e','b','b','b','b','b','e','b','e','b','e','b','b','e','b','e','b','b','b','b','e','b' },
            { 'b','e','b','e','e','e','e','e','e','e','e','e','e','e','b','e','e','e','b','e','e','e','b','e','e','e','e','e','e','e','e','e','e','e','b','e','b' },
            { 'b','e','b','e','b','b','e','b','e','b','e','b','b','b','b','b','b','e','b','e','b','b','b','b','b','b','e','b','e','b','e','b','b','e','b','e','b' },
            { 'b','e','e','e','e','e','e','b','e','b','p','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','p','b','e','b','e','e','e','e','e','e','b' },
            { 'b','b','b','b','b','b','b','b','t','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','t','b','b','b','b','b','b','b','b' },
        };
        SpawnGrid();
    }

    /// <summary>
    /// Deze methode gaat alle locaties van de grid af.
    /// </summary>
    void SpawnGrid()
    {
        for (int z = 0; z < breedte; z++)
            for (int x = 0; x < lengte; x++)
            {
                LoadBlock(gamegridd[x,z], z, x);
            } 
    }
    /// <summary>
    ///  Deze methode doet aan de hand wat er daar op in de grid staat het object laden.
    ///  En plaatst dat op de goeie plaats in het spel.
    /// </summary>
    /// <param name="TileType"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>
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
                    //Vanwege de methode waarin de grid wordt aangemaakt had ik 2 opties om de goeie teleporters te linken aan elkaar. 
                    //1 Was meerdere instances maken die al gelinkt waren en die dan laten spawnen.
                    //De andere was de linkcode in dit script handmatig afstellen. 
                    //Ik vond zelf dit een betere optie omdat het en minder verschillende blokken vereist en uiteindelijk code lijnen scheelt.
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
            case 'k':
                {
                    GameObject spawnPac = Instantiate(SpawnPacman, Vector3.zero, SpawnPacman.transform.rotation) as GameObject;
                    spawnPac.transform.parent = transform;
                    spawnPac.transform.localPosition = new Vector3(x, 1, z);
                    spawnPac.SetActive(true);
                }
                break;
            case 'c':
                {
                    GameObject citroen = Instantiate(Citroen, Vector3.zero, Citroen.transform.rotation) as GameObject;
                    citroen.transform.parent = transform;
                    citroen.transform.localPosition = new Vector3(x, 1, z);
                    citroen.SetActive(true);
                }
                break;
            default: break;
        }
    }
    private void Update()
    {
        Timer += Time.deltaTime;
        float val = Random.value;
        if (Timer > val * 10000)
        {
            Timer = 0;
            switch ((int)Random.Range(0, 4))
            {
                case 0: LoadBlock('c', 1, 1); break;
                case 1: LoadBlock('c', 35, 1); break;
                case 2: LoadBlock('c', 1, 21); break;
                case 3: LoadBlock('c', 35, 21); break;
            }
        }
    }
}
