using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script is responsable for making the blocks transparent when the orange is picked up.
/// It's inefficient as shit but it works.
/// Todo make this beter
/// </summary>
public class SinaasappelPowerup : MonoBehaviour {

    // Variable for measuring how long the powerup has been running for (in seconds)
    [SerializeField] Material transmaterial, defaulthMaterial;
    public float duration = 15;

    void OnTriggerStay(Collider col)
    {
        // Changes the flow of control based on the object with which the orange collides
        switch (col.gameObject.tag)
        {
            case "Pellet":
                // Destroys the pellet so that the orange can take its place without them overlapping
                Destroy(col);
                break;
            case "Player":
                ScoreCounter fruitscore = FindObjectOfType<ScoreCounter>();
                fruitscore.FruitPoints();
                gameObject.GetComponent<SphereCollider>().enabled = false;
                gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                StartCoroutine(Programma());
                sinaasTimer timerAnimation = FindObjectOfType<sinaasTimer>();
                timerAnimation.OrangeTimer();
                break;
        }
    }

    IEnumerator Programma()
    {
        Switches(0f, 0);
        yield return new WaitForSeconds((float) 8/9*duration);
        while (duration - ((float) 8/9*duration) > 0)
        {
            Switches(0f, 0);
            yield return new WaitForSeconds(0.25f);
            Switches(0.5f, 0);
            duration--;
            yield return new WaitForSeconds(0.25f);
        }
        Switches(1f,1);
    }

    public void Switches(float transparancy, int matrialuse)
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
}
