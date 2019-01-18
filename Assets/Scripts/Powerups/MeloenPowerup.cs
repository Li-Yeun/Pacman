using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MeloenPowerup : NetworkBehaviour
{

    public float duration = 15f;

    void OnTriggerEnter(Collider col)
    {
        if (!hasAuthority)
            return;
        //checks if collides with player or pellet, if collides with player then reverse the controls of the player
        switch (col.gameObject.tag)
        {
            case "Pellet":
                Destroy(col);
                break;
            case "Player":
                CmdCollision();
                break;
        }
    }

    [CommandAttribute]
    public void CmdCollision()
    {
        RpcCollision();
    }

    [ClientRpcAttribute]
    private void RpcCollision()
    {
        ScoreCounter fruitscore = FindObjectOfType<ScoreCounter>();
        fruitscore.FruitPoints();
        gameObject.GetComponent<SphereCollider>().enabled = false;
        foreach (MeshRenderer meshrender in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            meshrender.enabled = false;
        }
        foreach (Movement movement in FindObjectsOfType<Movement>())
        {
            movement.reversecontrols = true;
            StartCoroutine(Resett(movement));
        }
        if (FindObjectsOfType<melonTimer>().Length == 1)
        {
            melonTimer timerAnimation = FindObjectOfType<melonTimer>();
            timerAnimation.MelonTimer();
        }
    }

    IEnumerator Resett(Movement movement)
    {
        yield return new WaitForSeconds(duration);
        movement.reversecontrols = false;
        FindObjectOfType<PlayerOnline>().griddbased.Add(new Gridbased((int)gameObject.transform.localPosition.x, (int)gameObject.transform.localPosition.z));
        Destroy(gameObject);
    }

    public void Reset()
    {
        FindObjectOfType<PlayerOnline>().griddbased.Add(new Gridbased((int)gameObject.transform.localPosition.x, (int)gameObject.transform.localPosition.z));
        Destroy(gameObject);
    }
}