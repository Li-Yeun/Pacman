﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AppelPowerup : NetworkBehaviour
{

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
        PlayerOnline[] Player = FindObjectsOfType<PlayerOnline>();
        foreach (PlayerOnline player in Player)
        {
            player.SpawnDecoyBool = true;
        }
        ScoreCounter fruitscore = FindObjectOfType<ScoreCounter>();
        fruitscore.FruitPoints();
        Destroy(gameObject);
        if (FindObjectsOfType<appelTimer>().Length == 1)
        {
            appelTimer timerAnimation = FindObjectOfType<appelTimer>();
            timerAnimation.AppelTimer();
        }
    }

    public void Reset()
    {
        Destroy(gameObject);
    }
}
