﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PacmanCollision : NetworkBehaviour {

    [Tooltip("FX prefab on player")] [SerializeField] GameObject DeathFX;
    [SerializeField] Transform parent;
    GhostStates pacmanAttacking;


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
        if (gameObject.tag == "Player")
        {
            playerhealth Health = FindObjectOfType<playerhealth>();
            Health.DecreaseHealth();
            SendMessage("StartDeathSequence");
        }
        else
        {
            Destroy(gameObject);
            GameObject DecoyCamera = GameObject.FindGameObjectWithTag("Decoy Camera");
            Destroy(DecoyCamera);
        }
    }

    public void GhostInstantiated()
    {
        pacmanAttacking = FindObjectOfType<GhostStates>();
    }

}
