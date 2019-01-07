using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Deze class is verantwoordelijk voor het initialise van het level.
/// </summary>
public class Grid : MonoBehaviour
{
    private int TimesTeleporterCreated = 0;
    float Timer = 0;
    public GameObject block1;
    public GameObject pellet;
    public GameObject powerpill;
    public GameObject SlidingDoor;
    public GameObject Teleporter;
    public GameObject SpawnPacman;
    [Header ("Fruit")]
    public GameObject Citroen, Apple, Kers, Melon;
    [Header("Parent")]
    [SerializeField] Transform PelletsParent, SlidingDoorParent, TeleporterParent, BuildingBlockParent, PowerPillParent, CitroenParent, MelonParent, SpawnerParent, AppleParent, KersParent;
    public char[,] gamegrid;

    void Start()
    {
        // Dit is de level layout, hier staat in waar wat wordt geplaatst in de grid op het moment dat het level geladen wordt.
        gamegrid = new char[,]
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
            { 'b','b','e','b','b','f','b','b','b','b','e','e','b','e','b','e','b','b','k','b','b','e','b','e','b','e','e','b','b','b','b','f','b','b','e','b','b' },
            { 'b','e','e','e','e','e','e','e','e','b','e','b','b','e','b','e','b','s','s','s','b','e','b','e','b','b','e','b','e','e','e','e','e','e','e','e','b' },
            { 'b','p','b','b','b','b','b','b','e','e','e','e','e','e','b','e','b','s','s','s','b','e','e','e','e','e','e','e','e','b','b','b','b','b','b','p','b' },
            { 'b','e','e','e','e','e','e','e','e','b','e','b','b','e','b','e','b','b','b','b','b','e','b','e','b','b','e','b','e','e','e','e','e','e','e','e','b' },
            { 'b','b','e','b','b','f','b','b','b','b','e','e','b','e','b','e','e','e','a','e','e','e','b','e','b','e','e','b','b','b','b','f','b','b','e','b','b' },
            { 'b','b','e','b','e','e','e','e','e','b','b','e','b','e','b','e','b','b','b','b','b','e','b','e','b','e','b','b','e','e','e','e','e','b','e','b','b' },
            { 't','e','e','b','e','b','e','b','e','b','e','e','e','e','e','e','e','e','b','e','e','e','e','e','e','e','e','b','e','b','e','b','e','b','e','e','t' },
            { 'b','b','e','b','e','b','e','b','e','b','e','b','b','f','b','b','b','e','b','e','b','b','b','f','b','b','e','b','e','b','e','b','e','b','e','b','b' },
            { 'b','e','e','e','e','b','e','b','e','b','e','e','b','e','e','e','e','e','j','e','e','e','e','e','b','e','e','b','e','b','e','b','e','e','e','e','b' },
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
        for (int z = 0; z < gamegrid.GetLongLength(1); z++)
            for (int x = 0; x < gamegrid.GetLongLength(0); x++)
            {
                LoadBlock(gamegrid[x, z], z, x);
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
                    InstantiateObject(block1, x, z, BuildingBlockParent);
                }
                break;
            case 'k':
                {
                    InstantiateObject(Kers, x, z, KersParent);
                }
                break;
            case 'e':
                {
                    InstantiateObject(pellet, x, z, PelletsParent);
                }
                break;
            case 'p':
                {
                    InstantiateObject(powerpill, x, z, PowerPillParent);
                }
                break;
            case 'f':
                {
                    InstantiateObject(SlidingDoor, x, z, SlidingDoorParent);
                }
                break;
            case 't':
                {
                    TimesTeleporterCreated++;
                    GameObject Teleporterr = Instantiate(Teleporter, Vector3.zero, SlidingDoor.transform.rotation);
                    Teleporterr.transform.parent = TeleporterParent;
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
                }
                break;
            case 'j': // Werkt niet.
                {
                    InstantiateObject(SpawnPacman, x, z, SpawnerParent);
                }
                break;
            case 'c':
                {
                    InstantiateObject(Citroen, x, z, CitroenParent);
                }
                break;
            case 'a':
                {
                    InstantiateObject(Apple, x, z, AppleParent);
                }
                break;
            case 'm':
                {
                    InstantiateObject(Melon, x, z, MelonParent);
                }
                break;
            default: break;
        }
    }

    private void InstantiateObject(GameObject gameObject, int x, int z, Transform Parent)
    {
        GameObject gameObjectt = Instantiate(gameObject, Vector3.zero, gameObject.transform.rotation);
        gameObjectt.transform.parent = Parent;


        if (gameObject == block1)
        {
            gameObjectt.transform.localPosition = new Vector3(x, 1.5f, z);

            if (x == 0 || z == 0 || x == gamegrid.GetLongLength(1) - 1 || z == gamegrid.GetLongLength(0) - 1)
            { gameObjectt.tag = "BoundingWall"; }
            else
            {
                gameObjectt.tag = "Maze";
            }
        }
        else
            gameObjectt.transform.localPosition = new Vector3(x, 1, z);
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

    public void Reset()
    {
        if (GameObject.FindGameObjectWithTag("Fruit") != null)
        {
            foreach (GameObject Fruit in GameObject.FindGameObjectsWithTag("Fruit"))
            {
                Destroy(Fruit);
            }
        }
    }
}
