using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollision : MonoBehaviour
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
        //todo Serverbased maken
        switch (col.gameObject.tag)
        {
            case "Enemy": //Zorgt dat geestjes door elkaar kunnen bewegen
                {
                    Physics.IgnoreCollision(col.collider, GetComponent<Collider>());
                }
                break;
            case "Player":
                {  //Zorgt dat het geestje doodgaat als pacman een powerpill op heeft.
                    if (pacmanAttacking.IsVulnerable)
                    {
                        GameObject fx = Instantiate(DeathFX, transform.position, Quaternion.identity);
                        fx.transform.parent = Spawner;
                        gameObject.GetComponent<Movement>().dead = true;
                    }
                }
                break;
            default:
                break;
        }
    }

    public void GhostInstantiated()
    {
        pacmanAttacking = FindObjectOfType<GhostStates>();
    }

}