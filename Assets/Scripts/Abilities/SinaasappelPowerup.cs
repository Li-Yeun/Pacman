﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/// <summary>
/// This script is responsable for making the blocks transparent when the orange is eaten.
/// </summary>
public class SinaasappelPowerup : NetworkBehaviour
{
    public AudioSource poweruppickupsound;

    void Start()
    { poweruppickupsound = GetComponent<AudioSource>(); }
    // Variable for measuring how long the powerup has been running for (in seconds)
    [SerializeField] Material transmaterial, defaulthMaterial;
    [SerializeField] float duration = 15;

    void OnTriggerEnter(Collider col)
    {
        // Changes the flow of control based on the object with which the orange collides
        if (!hasAuthority)
            return;
        switch (col.gameObject.tag)
        {
            case "Pellet":
                // Destroys the pellet so that the orange can take its place without them overlapping
                Destroy(col);
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
        gameObject.GetComponent<SphereCollider>().enabled = false;
        foreach (MeshRenderer meshrender in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            meshrender.enabled = false;
        }
        StartCoroutine(Programma());
        if (FindObjectsOfType<sinaasTimer>().Length == 1)
        {
            sinaasTimer timerAnimation = FindObjectOfType<sinaasTimer>();
            timerAnimation.OrangeTimer();
        }
    }




    private IEnumerator Programma()
    {
        Switches(0f, 0);
        yield return new WaitForSeconds((float)8 / 9 * duration);
        while (duration - ((float)8 / 9 * duration) > 0)
        {
            Switches(0f, 0);
            yield return new WaitForSeconds(0.25f);
            Switches(0.5f, 0);
            duration--;
            yield return new WaitForSeconds(0.25f);
        }
        Switches(1f, 1);
        PlayerOnline[] Players = FindObjectsOfType<PlayerOnline>();
        foreach (PlayerOnline player in Players)
        {
            player.AddToGridList((int)gameObject.transform.localPosition.x, (int)gameObject.transform.localPosition.z);
        }
    }

    private void Switches(float transparancy, int matrialuse)
    {
        foreach (GameObject Maze in GameObject.FindGameObjectsWithTag("Maze"))
        {
            Renderer renderer = Maze.GetComponentInChildren<Renderer>();
            if (matrialuse == 0)
            {
                renderer.material = transmaterial;
            }
            else
            {
                renderer.material = defaulthMaterial;
                Destroy(gameObject);
            }
            renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, transparancy);
        }
    }

    public void Reset()
    {
        foreach (GameObject Maze in GameObject.FindGameObjectsWithTag("Maze"))
        {
            Renderer renderer = Maze.GetComponentInChildren<Renderer>();
            renderer.material = defaulthMaterial;
            Destroy(gameObject);
        }
    }
}