using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanCollision : MonoBehaviour {

    [Tooltip("FX prefab on player")] [SerializeField] GameObject DeathFX;
    [SerializeField] Transform parent;
    playerhealth Health;

    private void Start()
    {
        Health = FindObjectOfType<playerhealth>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                GameObject fx = Instantiate(DeathFX, transform.position, Quaternion.identity);
                fx.transform.parent = parent;
                Health.DecreaseHealth();
                SendMessage("StartDeathSequence");
                break;
            case "Friendly":
                break;
            default:
                break;
        }

    }
}
