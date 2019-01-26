﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class KersPowerup : NetworkBehaviour
{
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
    private void CmdCollision()
    {
        RpcCollision();
    }

    [ClientRpcAttribute]
    private void RpcCollision()
    {
        ScoreCounter fruitscore = FindObjectOfType<ScoreCounter>();
        fruitscore.FruitPoints();
        playerhealth Playerhealth = GameObject.FindObjectOfType<playerhealth>();
        Playerhealth.health++;
        if (FindObjectsOfType<kersTimer>().Length == 1)
        {
            kersTimer timerAnimation = FindObjectOfType<kersTimer>();
            timerAnimation.KersTimer();
        }
        PlayerOnline[] Players = FindObjectsOfType<PlayerOnline>();
        foreach (PlayerOnline player in Players)
        {
            player.AddToGridList((int)gameObject.transform.localPosition.x, (int)gameObject.transform.localPosition.z);
        }
        Destroy(gameObject);
    }

    public void Reset()
    {
        Destroy(gameObject);
    }



}