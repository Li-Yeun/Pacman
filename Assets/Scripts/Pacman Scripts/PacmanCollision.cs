using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PacmanCollision : NetworkBehaviour {

    [Tooltip("FX prefab on player")] [SerializeField] GameObject DeathFX;
    [SerializeField] Transform parent;
    GhostStates pacmanAttacking;
    playerhealth Health;

    private void Start()
    {
        Health = FindObjectOfType<playerhealth>();
       // pacmanAttacking = pacmanAttacking.GetComponent<PacmanAttacking>();
    }

    private void OnCollisionEnter(Collision collision)
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
            if (!pacmanAttacking.IsVulnerable)
                {
                    CmdDeathSequence();
                }
                break;
            case "Friendly":
                break;
            default:
                break;
        }

    }

    [CommandAttribute]
    private void CmdDeathSequence()
    {
        RpcDeathSequence();
    }
    [ClientRpcAttribute]
    private void RpcDeathSequence()
    {
        GameObject fx = Instantiate(DeathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Health.DecreaseHealth();
        SendMessage("StartDeathSequence");
    }

    public void GhostInstantiated()
    {
        pacmanAttacking = FindObjectOfType<GhostStates>();
    }

}
