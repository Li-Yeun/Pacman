using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanCollision : MonoBehaviour {

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
        //todo Serverbased maken
        switch (collision.gameObject.tag)
        {
            case "Enemy":
            if (!pacmanAttacking.IsVulnerable)
            {
                    GameObject fx = Instantiate(DeathFX, transform.position, Quaternion.identity);
                    fx.transform.parent = parent;
                    Health.DecreaseHealth();
                    SendMessage("StartDeathSequence");
            }
                break;
            case "Friendly":
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
