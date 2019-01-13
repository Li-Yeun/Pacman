using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DopplegangerCollision : NetworkBehaviour
{
    [Tooltip("FX prefab on player")] [SerializeField] GameObject DeathFX;
    private Transform Spawner;
    private GhostStates pacmanAttacking;

    private void Start()
    {
        Spawner = GameObject.Find("SpawnAtRunTime").transform;
    }

    void OnCollisionEnter(Collision col)
    {
        
        switch (col.gameObject.tag)
        {
            case "Player": //Zorgt dat geestjes door elkaar kunnen bewegen
                {
                    Physics.IgnoreCollision(col.collider, GetComponent<Collider>());
                }
                break;
            case "Enemy":
                {  //Zorgt dat het geestje doodgaat als pacman een powerpill op heeft.
                    CmdDeathSequence();
                }
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
        fx.transform.parent = Spawner;
        gameObject.GetComponent<Movement>().dead = true;
    }

    public void GhostInstantiated()
    {
        pacmanAttacking = FindObjectOfType<GhostStates>();
    }

}