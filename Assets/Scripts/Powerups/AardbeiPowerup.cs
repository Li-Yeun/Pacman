using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AardbeiPowerup : NetworkBehaviour
{

    public float duration = 15f;

    void OnTriggerEnter(Collider col)
    {
        if (!hasAuthority)
            return;
        switch (col.gameObject.tag)
        {
            case "Pellet":
                Destroy(col.gameObject);
                break;
            case "Player":
                Debug.Log(5);
                CmdCollision(col.gameObject);
                break;
        }
    }

    [CommandAttribute]
    public void CmdCollision(GameObject col)
    {
        RpcCollision(col);
    }

    [ClientRpcAttribute]
    private void RpcCollision(GameObject col)
    {
        ScoreCounter fruitscore = FindObjectOfType<ScoreCounter>();
        fruitscore.FruitPoints();
        gameObject.GetComponentInChildren<SphereCollider>().enabled = false;
        foreach (MeshRenderer meshrender in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            meshrender.enabled = false;
        }
        PacmanMovement pacmanMovement = col.GetComponent<PacmanMovement>();
        if (FindObjectsOfType<aardbeiTimer>().Length == 1)
        {
            aardbeiTimer timerAnimation = FindObjectOfType<aardbeiTimer>();
            timerAnimation.AardbeiTimer();
        }
        StartCoroutine(Resett(pacmanMovement));
    }

    private IEnumerator Resett(PacmanMovement pacmanMovement)
    {
        pacmanMovement.Speed.x++;
        pacmanMovement.Speed.z++;
        yield return new WaitForSeconds(duration);
        pacmanMovement.Speed.x--;
        pacmanMovement.Speed.z--;
        Destroy(gameObject);
    }

}