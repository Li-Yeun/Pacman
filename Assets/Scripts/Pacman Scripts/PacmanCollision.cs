using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PacmanCollision : NetworkBehaviour {

    [Tooltip("FX prefab on player")] [SerializeField] GameObject DeathFX;  // Particle effect als pacman doodgaat
    [SerializeField] Transform parent;
    [SerializeField] GameObject[] FireWorks;                               // Vuurwerk effect als pacman doodgaat
    private GhostStates pacmanAttacking;                                   // De state van de ghost of ze vulnerable zijn
    private HUD UI;

    void Start()
    {
        pacmanAttacking = FindObjectOfType<GhostStates>();
        UI = FindObjectOfType<HUD>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasAuthority)
            return;

        switch (collision.gameObject.tag)
        {
            case "Player":
            case "Decoy":
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());              
                break;
            case "Enemy":
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
                StartCoroutine(ResetCollisionDetection(collision));
                if (!pacmanAttacking.IsVulnerable)                      // Checken of pacman of het geestjes dood moet gaan gebaseerd op de state van het geestje
                {
                    CmdDeathSequence(collision.gameObject);
                }
                break;
            case "Friendly":
                break;
            default:
                break;
        }

    }

    private IEnumerator ResetCollisionDetection(Collision collision)
    {
        yield return new WaitForSeconds(1f);
        Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(),false);
    }

    [CommandAttribute]
    private void CmdDeathSequence(GameObject Ghost)
    {
        RpcDeathSequence(Ghost);
    }

    [ClientRpcAttribute]
    private void RpcDeathSequence(GameObject Ghost)
    {
        GameObject fx = Instantiate(DeathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        if (gameObject.tag == "Player")
        {
            // Checken welke UI en vuurwerk kleur moeten gebruiken als pacman door een geestje dood gaat
            switch (Ghost.GetComponent<Movement>().name)
            {
                case "Blue":
                    KillText("Blue");
                    Instantiate(FireWorks[0]);
                    break;
                case "Red":
                    KillText("Red");
                    Instantiate(FireWorks[1]);
                    break;
                case "Pink":
                    KillText("Pink");
                    Instantiate(FireWorks[2]);
                    break;
                case "Orange":
                    KillText("Orange");
                    Instantiate(FireWorks[3]);
                    break;
            }
            playerhealth Health = FindObjectOfType<playerhealth>();
            Health.DecreaseHealth();
            SendMessage("StartDeathSequence");
        }
        else   //Dit is wat er moet gebeuren als de decoy een geest raakt
        {
            GameObject DecoyCamera = GameObject.FindGameObjectWithTag("Decoy Camera");
            Destroy(DecoyCamera);
            Destroy(gameObject);
        }
        
    }

    private void KillText(string Ghost)
    {
        UI.CollisionText.SetActive(true);
        UI.CollisionText.GetComponent<DeathFeed>().GhostKilledPacman(Ghost);
    }

    public void GhostInstantiated()
    {
        pacmanAttacking = FindObjectOfType<GhostStates>();
    }

}
