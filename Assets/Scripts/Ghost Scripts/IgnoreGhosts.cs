using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreGhosts : MonoBehaviour
{
    [Tooltip("FX prefab on player")] [SerializeField] GameObject DeathFX;
    Transform Spawner;
    public PacmanAttacking pacmanAttacking;
    private void Start()
    {
        Spawner = GameObject.Find("SpawnAtRunTime").transform;
        pacmanAttacking = pacmanAttacking.GetComponent<PacmanAttacking>();
    }

    void OnCollisionEnter(Collision col)
    {
        
        switch (col.gameObject.tag) {
            case "Enemy": //Zorgt dat geestjes door elkaar kunnen bewegen
            {
                    Physics.IgnoreCollision(col.collider, GetComponent<Collider>());
            }
                break;
            case "Player":
                {  //Zorgt dat het geestje doodgaat als pacman een powerpill op heeft.
                    if (pacmanAttacking.PacmanIsTheBoyInTown)
                    {
                        GameObject fx = Instantiate(DeathFX, transform.position, Quaternion.identity);
                        fx.transform.parent = Spawner;
                        transform.position = Spawner.transform.position;
                    }
                }
                break;
            default:
                break;
    }   }
}
