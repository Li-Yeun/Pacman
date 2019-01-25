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
    private int counter = 0, TimesTeleporterCreated = 0;
    // De muren van het spel
    [SerializeField] GameObject block1, block2, block3, block4, block5, block6, block7;

    // De gele bolletjes die pacman op eet.
    [SerializeField] GameObject pellet;

    // De pil die zorgt dat pacman de geestjes op kan eten ipv an andersom.
    [SerializeField] GameObject powerpill;

    // De teleporters aan de zijkant van de map.
    [SerializeField] GameObject Teleporter;

    // De spawnlocatwion van Pacman (werkt alleen voor respawns for some reason niet de eerste spawn) TODO
    [SerializeField] GameObject SpawnPacman;

    /// <summary>
    /// Citroen maakt Pacman sneller
    /// Apple laat pacman onder de grond / onzichtbaar gaan
    /// Kers geeft Pacman één leven extra
    /// Melon tbd
    /// ---
    /// </summary>
    [Header("Fruit")]
    [SerializeField] public GameObject Kers, Aardbei, Appel, Meloen, Sinaasappel;

    /// <summary>
    /// Dit zijn de locations waarin de instances gezet worden deze zijn alleen ter sortering verder niks.
    /// </summary>
    [Header("Parent")]
    [SerializeField] public Transform PelletsParent, TeleporterParent, BuildingBlockParent, PowerPillParent, SpawnerParent;
    [Header("Parent fruits")]
    [SerializeField] public Transform KersParent, AardbeiParent, AppelParent, MeloenParent, SinaasappelParent;

    public char[,] gamegrid;
    #endregion

    void Start()
    {
        // Dit is de level layout, hier staat in waar wat wordt geplaatst in de grid op het moment dat het level geladen wordt.
        gamegrid = new char[,]
        {
            { 'b','b','b','b','b','b','b','b','t','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','t','b','b','b','b','b','b','b','b' },
            { 'b','s','e','e','e','e','e','b','e','b','p','e','e','e','e','e','e','e','b','e','e','e','e','e','e','e','p','b','e','b','e','e','e','e','e','s','b' },
            { 'b','e','b','b','b','b','e','e','e','b','e','b','b','e','b','b','b','e','b','e','b','b','b','e','b','b','e','b','e','e','e','b','b','b','b','e','b' },
            { 'b','e','b','b','e','e','e','b','e','b','e','b','b','e','b','b','b','e','b','e','b','b','b','e','b','b','e','b','e','b','e','e','e','b','b','e','b' },
            { 'b','e','b','b','e','b','b','b','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','b','b','b','e','b','b','e','b' },
            { 'b','e','e','e','e','b','b','b','e','b','e','b','b','e','b','e','b','b','b','b','b','e','b','e','b','b','e','b','e','b','b','b','e','e','e','e','b' },
            { 'b','b','e','b','e','e','e','e','e','b','e','b','b','e','b','e','e','e','b','e','e','e','b','e','b','b','e','b','e','e','e','e','e','b','e','b','b' },
            { 't','e','e','b','e','b','b','b','e','b','s','e','e','e','b','e','b','e','e','e','b','e','b','e','e','e','s','b','e','b','b','b','e','b','e','e','t' },
            { 'b','b','e','b','e','e','e','e','s','b','b','e','b','e','b','e','b','b','e','b','b','e','b','e','b','e','b','b','s','e','e','e','e','b','e','b','b' },
            { 'b','b','e','b','b','e','b','b','b','b','e','e','b','e','b','e','b','e','e','s','b','e','b','e','b','e','e','b','b','b','b','e','b','b','e','b','b' },
            { 'b','e','e','e','e','e','e','e','e','b','e','b','b','e','b','e','b','e','b','e','b','e','b','e','b','b','e','b','e','e','e','e','e','e','e','e','b' },
            { 'b','p','b','b','b','b','b','b','e','e','e','e','e','e','b','e','b','s','e','e','b','e','e','e','e','e','e','e','e','b','b','b','b','b','b','p','b' },
            { 'b','e','e','e','e','e','e','e','e','b','e','b','b','e','b','e','b','b','e','b','b','e','b','e','b','b','e','b','e','e','e','e','e','e','e','e','b' },
            { 'b','b','e','b','b','e','b','b','b','b','e','e','b','e','b','e','e','e','e','e','e','e','b','e','b','e','e','b','b','b','b','e','b','b','e','b','b' },
            { 'b','b','e','b','e','e','e','e','s','b','b','e','b','e','b','e','b','b','b','b','b','e','b','e','b','e','b','b','s','e','e','e','e','b','e','b','b' },
            { 't','e','e','b','e','b','e','b','e','b','s','e','e','e','e','e','e','e','b','e','e','e','e','e','e','e','s','b','e','b','e','b','e','b','e','e','t' },
            { 'b','b','e','b','e','b','e','b','e','b','e','b','b','e','b','b','b','e','b','e','b','b','b','e','b','b','e','b','e','b','e','b','e','b','e','b','b' },
            { 'b','e','e','e','e','b','e','b','e','b','e','e','b','e','e','e','e','e','e','e','e','e','e','e','b','e','e','b','e','b','e','b','e','e','e','e','b' },
            { 'b','e','b','b','b','b','e','b','e','b','b','e','b','e','b','e','b','b','b','b','b','e','b','e','b','e','b','b','e','b','e','b','b','b','b','e','b' },
            { 'b','e','b','e','e','e','e','e','e','e','e','e','e','e','b','e','e','e','b','e','e','e','b','e','e','e','e','e','e','e','e','e','e','e','b','e','b' },
            { 'b','e','b','e','b','b','e','b','e','b','e','b','b','b','b','b','b','e','b','e','b','b','b','b','b','b','e','b','e','b','e','b','b','e','b','e','b' },
            { 'b','s','e','e','e','e','e','b','e','b','p','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','p','b','e','b','e','e','e','e','e','s','b' },
            { 'b','b','b','b','b','b','b','b','t','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','b','t','b','b','b','b','b','b','b','b' },
        };
        SpawnGrid();
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
            case 'e':
                {
                    InstantiateObject(pellet, x, z, PelletsParent);
                }
                break;
            case 's':
                {
                }
                break;
            case 'p':
                {
                    InstantiateObject(powerpill, x, z, PowerPillParent);
                }
                break;
            case 't':
                {
                    TimesTeleporterCreated++;
                    GameObject Teleporterr = Instantiate(Teleporter, Vector3.zero, TeleporterParent.transform.rotation);
                    Teleporterr.transform.parent = TeleporterParent;
                    Teleporterr.transform.localPosition = new Vector3(x, 1, z);
                    TeleportScript2 TeleporterScript = Teleporterr.GetComponent<TeleportScript2>();
                    //Vanwege de methode waarin de grid wordt aangemaakt had ik 2 opties om de goeie teleporters te linken aan elkaar. 
                    //1 Was meerdere instances maken die al gelinkt waren en die dan laten spawnen.
                    //De andere was de linkcode in dit script handmatig afstellen. 
                    //Ik vond zelf dit een betere optie omdat het en minder verschillende blokken vereist en uiteindelijk code lijnen scheelt.
                    switch (TimesTeleporterCreated)
                    {
                        default: TeleporterScript.code = 0; Teleporterr.transform.localRotation = Quaternion.Euler(0f, 270f, 0f); break; 
                        case 1: TeleporterScript.code = 1; Teleporterr.transform.localRotation = Quaternion.Euler(0f,90f,0f); break;
                        case 2: TeleporterScript.code = 0; Teleporterr.transform.localRotation = Quaternion.Euler(0f, 90f, 0f); break;
                        case 3: TeleporterScript.code = 3; break; //Werkt zo al.
                        case 4: TeleporterScript.code = 3; Teleporterr.transform.localRotation = Quaternion.Euler(0f, 180f, 0f); break;
                        case 5: TeleporterScript.code = 2; break; // Werkt zo al.
                        case 6: TeleporterScript.code = 2; Teleporterr.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);    break;
                        case 7: TeleporterScript.code = 1; Teleporterr.transform.localRotation = Quaternion.Euler(0f, 270f, 0f); break;
                    }
                }
                break;
            case 'j':
                {
                    InstantiateObject(SpawnPacman, x, z, SpawnerParent);
                }
                break;
            case 'w':
                {
                    InstantiateObject(Aardbei, x, z, AardbeiParent);
                }
                break;
            case 'a':
                {
                    InstantiateObject(Appel, x, z, AppelParent);
                }
                break;
            case 'o':
                {
                    InstantiateObject(Sinaasappel, x, z, SinaasappelParent);
                }
                break;
            case 'm':
                {
                    InstantiateObject(Meloen, x, z, MeloenParent);
                }
                break;
            case 'k':
                {
                    InstantiateObject(Kers, x, z, KersParent);
                }
                break;
            default: break;
        }
    }
    #endregion 

    private void InstantiateObject(GameObject gameObject, int x, int z, Transform Parent)
    {

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
                int chosenblock = 0;
                if (gamegrid[z, x - 1] != 'b' && gamegrid[z, x + 1] != 'b' && gamegrid[z - 1, x] == 'b' && gamegrid[z + 1, x] != 'b') { chosenblock = 1; }
                else if (gamegrid[z, x - 1] != 'b' && gamegrid[z, x + 1] == 'b' && gamegrid[z - 1, x] == 'b' && gamegrid[z + 1, x] != 'b') { chosenblock = 2; }
                else if (gamegrid[z, x - 1] != 'b' && gamegrid[z, x + 1] == 'b' && gamegrid[z - 1, x] == 'b' && gamegrid[z + 1, x] == 'b') { chosenblock = 3; }
                else if (gamegrid[z, x - 1] != 'b' && gamegrid[z, x + 1] == 'b' && gamegrid[z - 1, x] != 'b' && gamegrid[z + 1, x] != 'b') { chosenblock = 4; }
                else if (gamegrid[z, x - 1] != 'b' && gamegrid[z, x + 1] == 'b' && gamegrid[z - 1, x] != 'b' && gamegrid[z + 1, x] == 'b') { chosenblock = 5; }
                else if (gamegrid[z, x - 1] == 'b' && gamegrid[z, x + 1] == 'b' && gamegrid[z - 1, x] != 'b' && gamegrid[z + 1, x] == 'b') { chosenblock = 6; }
                else if (gamegrid[z, x - 1] != 'b' && gamegrid[z, x + 1] != 'b' && gamegrid[z - 1, x] != 'b' && gamegrid[z + 1, x] == 'b') { chosenblock = 7; }
                else if (gamegrid[z, x - 1] == 'b' && gamegrid[z, x + 1] != 'b' && gamegrid[z - 1, x] != 'b' && gamegrid[z + 1, x] == 'b') { chosenblock = 8; }
                else if (gamegrid[z, x - 1] == 'b' && gamegrid[z, x + 1] != 'b' && gamegrid[z - 1, x] == 'b' && gamegrid[z + 1, x] == 'b') { chosenblock = 9; }
                else if (gamegrid[z, x - 1] == 'b' && gamegrid[z, x + 1] != 'b' && gamegrid[z - 1, x] != 'b' && gamegrid[z + 1, x] != 'b') { chosenblock = 10; }
                else if (gamegrid[z, x - 1] == 'b' && gamegrid[z, x + 1] != 'b' && gamegrid[z - 1, x] == 'b' && gamegrid[z + 1, x] != 'b') { chosenblock = 11; }
                else if (gamegrid[z, x - 1] == 'b' && gamegrid[z, x + 1] == 'b' && gamegrid[z - 1, x] == 'b' && gamegrid[z + 1, x] != 'b') { chosenblock = 12; }
                else if (gamegrid[z, x - 1] == 'b' && gamegrid[z, x + 1] == 'b' && gamegrid[z - 1, x] != 'b' && gamegrid[z + 1, x] != 'b') { chosenblock = 13; }
                else if (gamegrid[z, x - 1] != 'b' && gamegrid[z, x + 1] != 'b' && gamegrid[z - 1, x] == 'b' && gamegrid[z + 1, x] == 'b') { chosenblock = 14; }
                else if (gamegrid[z, x - 1] == 'b' && gamegrid[z, x + 1] == 'b' && gamegrid[z - 1, x] == 'b' && gamegrid[z + 1, x] == 'b') { chosenblock = 15; }
                else if (gamegrid[z, x - 1] != 'b' && gamegrid[z, x + 1] != 'b' && gamegrid[z - 1, x] != 'b' && gamegrid[z + 1, x] != 'b') { chosenblock = 16; }

                switch (chosenblock) {
                    case 1:
                        gameObjectt = Instantiate(block3, Vector3.zero, gameObject.transform.rotation);
                        gameObjectt.transform.eulerAngles = new Vector3(0, 90, 0);
                        break;
                    case 4:
                        gameObjectt = Instantiate(block3, Vector3.zero, gameObject.transform.rotation);
                        gameObjectt.transform.eulerAngles = new Vector3(0, 180, 0);
                        break;
                    case 7:
                        gameObjectt = Instantiate(block3, Vector3.zero, gameObject.transform.rotation);
                        gameObjectt.transform.eulerAngles = new Vector3(0, 270, 0);
                        break;
                    case 10:
                        gameObjectt = Instantiate(block3, Vector3.zero, gameObject.transform.rotation);
                        break;
                    case 13:
                        gameObjectt = Instantiate(block4, Vector3.zero, gameObject.transform.rotation);
                        break;
                    case 14:
                        gameObjectt = Instantiate(block4, Vector3.zero, gameObject.transform.rotation);
                        gameObjectt.transform.eulerAngles = new Vector3(0, 90, 0);
                        break;
                    case 2:
                        gameObjectt = Instantiate(block5, Vector3.zero, gameObject.transform.rotation);
                        gameObjectt.transform.eulerAngles = new Vector3(0, 180, 0);
                        break;
                    case 8:
                        gameObjectt = Instantiate(block5, Vector3.zero, gameObject.transform.rotation);

                        break;
                    case 5:
                        gameObjectt = Instantiate(block5, Vector3.zero, gameObject.transform.rotation);
                        gameObjectt.transform.eulerAngles = new Vector3(0, 270, 0);
                        break;
                    case 11:
                        gameObjectt = Instantiate(block5, Vector3.zero, gameObject.transform.rotation);
                        gameObjectt.transform.eulerAngles = new Vector3(0, 90, 0);
                        break;
                    case 3:
                        gameObjectt = Instantiate(block6, Vector3.zero, gameObject.transform.rotation);
                        gameObjectt.transform.eulerAngles = new Vector3(0, 270, 0);
                        break;
                    case 9:
                        gameObjectt = Instantiate(block6, Vector3.zero, gameObject.transform.rotation);
                        gameObjectt.transform.eulerAngles = new Vector3(0, 90, 0);
                        break;
                    case 6:
                        gameObjectt = Instantiate(block6, Vector3.zero, gameObject.transform.rotation);
                        break;
                    case 12:
                        gameObjectt = Instantiate(block6, Vector3.zero, gameObject.transform.rotation);
                        gameObjectt.transform.eulerAngles = new Vector3(0, 180, 0);
                        break;
                    case 15:
                        gameObjectt = Instantiate(block7, Vector3.zero, gameObject.transform.rotation);
                        break;
                    case 16:
                        gameObjectt = Instantiate(block1, Vector3.zero, gameObject.transform.rotation);
                        break;
                    default:
                        gameObjectt = Instantiate(powerpill, Vector3.zero, gameObject.transform.rotation);
                        break;
                }
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
            if(gameObjectt.CompareTag("Pellet"))
            {
                gameObjectt.transform.localPosition = new Vector3(x, 1.2f, z);
            } else
            gameObjectt.transform.localPosition = new Vector3(x, 1, z);
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
