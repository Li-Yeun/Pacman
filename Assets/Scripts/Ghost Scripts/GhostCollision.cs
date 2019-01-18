using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GhostCollision : NetworkBehaviour
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
        if (!hasAuthority)
            return;
        switch (col.gameObject.tag)
        {
            case "Enemy": //Zorgt dat geestjes door elkaar kunnen bewegen
                {
                    Physics.IgnoreCollision(col.collider, GetComponent<Collider>());
                }
                break;
            case "Player":
                {  //Zorgt dat het geestje doodgaat als pacman een powerpill op heeft.
                    Physics.IgnoreCollision(col.collider, GetComponent<Collider>());
                    StartCoroutine(ResetCollisionDetection(col));
                    if (pacmanAttacking.IsVulnerable)
                    {
                        CmdDeathSequence();
                    }
                }
                break;
            default:
                break;
        }
    }

    IEnumerator ResetCollisionDetection(Collision col)
    {
        yield return new WaitForSeconds(1f);
        Physics.IgnoreCollision(col.collider, GetComponent<Collider>(), false);
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