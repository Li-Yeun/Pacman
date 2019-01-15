using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AppelPowerup : NetworkBehaviour
{

    public float duration = 15f;

    public GameObject Doppelganger;

    [SerializeField] Transform parent;

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
                Debug.Log(4);
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
        Destroy(gameObject);
        if (FindObjectsOfType<appelTimer>().Length == 1)
        {
            appelTimer timerAnimation = FindObjectOfType<appelTimer>();
            timerAnimation.AppelTimer();
        }
    }



}
