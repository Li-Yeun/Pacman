using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitChecker : MonoBehaviour
{
    /// <summary>
    /// Checks if there already is a power-up at the spot where a new power-up spawns
    /// To ensure that no more than one power-up can spawn on one space on the grid
    /// If two power-ups spawn on the same spot on the grid, one of the power-ups is deleted
    /// </summary>
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
