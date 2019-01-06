using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitChecker : MonoBehaviour
{
    void Start()
    {
        foreach (FruitChecker cp in FindObjectsOfType<FruitChecker>())
        {
            if (cp.transform.position.x == gameObject.transform.position.x && cp.transform.position.y == gameObject.transform.position.y && cp != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
