using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreGhosts : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Physics.IgnoreCollision(col.collider, GetComponent<Collider>());
        }
    }
}
