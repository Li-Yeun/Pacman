using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisingWater : MonoBehaviour
{

    [SerializeField] Vector3 StartOffset;           // De mutatie van hoever de water vanuit zijn begin positie moet staan
    [SerializeField] float Speed;                   // De snelheid waarmee het water naar boven/beneden komt
    public bool Lock = false;                       // De boolean die ervoor zorgt dat het water naar boven of naar beneden moet gaan
    private Vector3 defaulthPos;                    // De begin postite van het water in de wereld voordat er een mutatie plaats vindt

    void Start()
    {
        defaulthPos = gameObject.transform.position;
        gameObject.transform.position -= StartOffset;
    }

    void Update()
    {
        if (Lock == false)
        {
            // Wanneer de huidige y-positie van het water kleiner is dan de originele y-posistie, dan wordt het water langzaam naar boven gebracht
            if (gameObject.transform.position.y < defaulthPos.y)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + Speed * Time.deltaTime, gameObject.transform.position.z);
            }
            else
            {
                gameObject.transform.position = defaulthPos;
            }
        }
        else
        {
            // Wanneer de Lock true is wordt de water naar benden gebracht totdat hij zijn begin postie bereikt heeft.
            if (gameObject.transform.position.y >= defaulthPos.y - StartOffset.y)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - Speed * Time.deltaTime * 2, gameObject.transform.position.z);
            }
            else
            {
                gameObject.transform.position = defaulthPos - StartOffset;
            }
        }
    }

    public void Reset()
    {
        Destroy(gameObject);
    }
}