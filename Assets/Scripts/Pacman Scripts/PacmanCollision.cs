using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PacmanCollision : NetworkBehaviour {

    [Tooltip("FX prefab on player")] [SerializeField] GameObject DeathFX;
    [SerializeField] Transform parent;
    [SerializeField] GameObject[] FireWorks;
    private GhostStates pacmanAttacking;
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
                if (!pacmanAttacking.IsVulnerable)
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
        else
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
