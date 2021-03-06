﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/// <summary>
/// De appel spawned een decoy, als pacman deze opeet.
/// </summary>
public class AppelPowerup : NetworkBehaviour
{
    public AudioSource poweruppickupsound;

    void Start()
    { poweruppickupsound = GetComponent<AudioSource>(); }

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
                poweruppickupsound.Play();
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
        PlayerOnline[] Players = FindObjectsOfType<PlayerOnline>();
        foreach (PlayerOnline player in Players)
        {
            player.AddToGridList((int)gameObject.transform.localPosition.x, (int)gameObject.transform.localPosition.z);
        }
    }

    public void Reset()
    {
        Destroy(gameObject);
    }
}
