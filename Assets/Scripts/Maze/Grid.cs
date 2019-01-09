using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Deze class is verantwoordelijk voor het initialise van het level. 
/// Dit doet hij door instances te maken van voorwerpen en die op de juiste plaats van het grid erin te zetten.
/// </summary>
public class Grid : MonoBehaviour
{
    #region Base Values

    private int TimesTeleporterCreated = 0;
    // De muren van het spel
    public GameObject block1;
    public GameObject block2;

    // De gele bolletjes die pacman op eet.
    public GameObject pellet;

    // De pil die zorgt dat pacman de geestjes op kan eten ipv an andersom.
    public GameObject powerpill;

    // De deuren die alleen pacman kan bedienen.
    public GameObject SlidingDoor;

    // De teleporters aan de zijkant van de map.
    public GameObject Teleporter;

    // De spawnlocatwion van Pacman (werkt alleen voor respawns for some reason niet de eerste spawn) TODO
    public GameObject SpawnPacman;
    
    /// <summary>
    /// Citroen maakt Pacman sneller
    /// Apple laat pacman onder de grond / onzichtbaar gaan
    /// Kers geeft Pacman één leven extra
    /// Melon tbd
    /// ---
    /// </summary>
    [Header ("Fruit")]
    public GameObject Citroen, Apple, Kers, Melon, Orange;

    /// <summary>
    /// Dit zijn de locations waarin de instances gezet worden deze zijn alleen ter sortering verder niks.
    /// </summary>
    [Header("Parent")]
    [SerializeField] Transform PelletsParent, SlidingDoorParent, TeleporterParent, BuildingBlockParent, PowerPillParent, CitroenParent, MelonParent, SpawnerParent, AppleParent, KersParent, OrangeParent;
    public char[,] gamegrid;
    #endregion

    void Start()
    {
        // Dit is de level layout, hier staat in waar wat wordt geplaatst in de grid op het moment dat het level geladen wordt.
        gamegrid = new char[,]
        {
            { 'b','b','b','b','b','b','b','b','t','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','t','b','b','b','b','b','b','b','b' },
            { 'b','e','s','e','e','e','e','b','e','b','p','e','e','e','e','e','e','e','b','e','e','e','e','e','e','e','p','b','e','b','e','e','e','e','e','e','b' },
            { 'b','s','b','b','b','b','e','e','e','b','e','b','b','e','b','b','b','e','b','e','b','b','b','e','b','b','e','b','e','e','e','b','b','b','b','e','b' },
            { 'b','e','b','b','e','e','e','b','e','b','e','b','b','e','b','b','b','e','b','e','b','b','b','e','b','b','e','b','e','b','e','e','e','b','b','e','b' },
            { 'b','e','b','b','e','b','b','b','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','b','b','b','e','b','b','e','b' },
            { 'b','e','e','e','e','b','b','b','e','b','e','b','b','e','b','e','b','b','b','b','b','e','b','e','b','b','e','b','e','b','b','b','e','e','e','e','b' },
            { 'b','b','e','b','e','e','e','e','e','b','e','b','b','e','b','e','e','e','b','e','e','e','b','e','b','b','e','b','e','e','e','e','e','b','e','b','b' },
            { 't','e','e','b','e','b','b','b','e','b','e','e','e','e','b','e','b','e','b','e','b','e','b','e','e','e','e','b','e','b','b','b','e','b','e','e','t' },
            { 'b','b','e','b','e','e','e','e','e','b','b','e','b','e','b','e','b','e','e','e','b','e','b','e','b','e','b','b','e','e','e','e','e','b','e','b','b' },
            { 'b','b','e','b','b','f','b','b','b','b','e','e','b','e','b','e','b','b','e','b','b','e','b','e','b','e','e','b','b','b','b','f','b','b','e','b','b' },
            { 'b','e','e','e','e','e','e','e','e','b','e','b','b','e','b','e','b','s','s','s','b','e','b','e','b','b','e','b','e','e','e','e','e','e','e','e','b' },
            { 'b','p','b','b','b','b','b','b','e','e','e','e','e','e','b','e','b','s','s','s','b','e','e','e','e','e','e','e','e','b','b','b','b','b','b','p','b' },
            { 'b','e','e','e','e','e','e','e','e','b','e','b','b','e','b','e','b','b','b','b','b','e','b','e','b','b','e','b','e','e','e','e','e','e','e','e','b' },
            { 'b','b','e','b','b','f','b','b','b','b','e','e','b','e','b','e','e','e','e','e','e','e','b','e','b','e','e','b','b','b','b','f','b','b','e','b','b' },
            { 'b','b','e','b','e','e','e','e','e','b','b','e','b','e','b','e','b','b','b','b','b','e','b','e','b','e','b','b','e','e','e','e','e','b','e','b','b' },
            { 't','e','e','b','e','b','e','b','e','b','e','e','e','e','e','e','e','e','b','e','e','e','e','e','e','e','e','b','e','b','e','b','e','b','e','e','t' },
            { 'b','b','e','b','e','b','e','b','e','b','e','b','b','f','b','b','b','e','b','e','b','b','b','f','b','b','e','b','e','b','e','b','e','b','e','b','b' },
            { 'b','e','e','e','e','b','e','b','e','b','e','e','b','e','e','e','e','e','e','e','e','e','e','e','b','e','e','b','e','b','e','b','e','e','e','e','b' },
            { 'b','e','b','b','b','b','e','b','e','b','b','e','b','e','b','e','b','b','b','b','b','e','b','e','b','e','b','b','e','b','e','b','b','b','b','e','b' },
            { 'b','e','b','e','e','e','e','e','e','e','e','e','e','e','b','e','e','e','b','e','e','e','b','e','e','e','e','e','e','e','e','e','e','e','b','e','b' },
            { 'b','e','b','e','b','b','e','b','e','b','e','b','b','b','b','b','b','e','b','e','b','b','b','b','b','b','e','b','e','b','e','b','b','e','b','e','b' },
            { 'b','e','e','e','e','e','e','b','e','b','p','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','p','b','e','b','e','e','e','e','e','e','b' },
            { 'b','b','b','b','b','b','b','b','t','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','t','b','b','b','b','b','b','b','b' },
        };
        SpawnGrid();
        InvokeRepeating("SpawnRandomFruit", Random.Range(0, 10), Random.Range(0, 10));
    }

    #region GridConverter
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
            case 's':
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
            case 'j': 
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
            case 'o':
                {
                    InstantiateObject(Orange, x, z, OrangeParent);
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
    #endregion 

    private void InstantiateObject(GameObject gameObject, int x, int z, Transform Parent) { 

    GameObject gameObjectt;
        //Spawncode voor de blokken.
        if (gameObject == block1)
        {
            // Zorgt zodat de muren aan de zijkant van het veld een andere tag hebben zodat pacman niet uit het veld kan.
            if (x == 0 || z == 0 || x == gamegrid.GetLongLength(1) - 1 || z == gamegrid.GetLongLength(0) - 1)
            {

                // Zorgt voor een mooie rotatie van de meest buitentste blokken.
                gameObjectt = Instantiate(block2, Vector3.zero, gameObject.transform.rotation);
                if (x == 0 || x == gamegrid.GetLongLength(1) - 1)
                {
                    gameObjectt.transform.eulerAngles = new Vector3(0, 90, 0);
                }
                gameObjectt.tag = "BoundingWall";
            }
            else
            {
                gameObjectt = Instantiate(gameObject, Vector3.zero, gameObject.transform.rotation);
                gameObjectt.tag = "Maze";
            }
            gameObjectt.transform.parent = Parent;
            gameObjectt.transform.localPosition = new Vector3(x, 1f, z);
        }
        // Spawn code voor alles behalve de blokken.
        else
        {
            gameObjectt = Instantiate(gameObject, Vector3.zero, gameObject.transform.rotation);
            gameObjectt.transform.parent = Parent;
            gameObjectt.transform.localPosition = new Vector3(x, 1, z);
        }

    }
    bool Spawned = true;
    private void SpawnRandomFruit()
    {
        Spawned = false;
        for (int z = 0; z < gamegrid.GetLongLength(1); z++)
            for (int x = 0; x < gamegrid.GetLongLength(0); x++)
            {
                Check(gamegrid[x, z], z, x);
                if (Spawned)
                {
                    return;
                }
            }
    }
    private void Check(char TileType, int x, int y)
    {
        if (TileType == 's')
        {
            switch ((int)Random.Range(0, 4))
            {
                case 0: LoadBlock('c', x, y); break;
                case 1: LoadBlock('k', x, y); break;
                case 2: LoadBlock('m', x, y); break;
                case 3: LoadBlock('o', x, y); break;
            }
            gamegrid[y,x] = 'd';
            Spawned = true;
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
