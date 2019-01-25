using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpUniversalScript : MonoBehaviour
{
    [SerializeField] Material transmaterial, defaulthMaterial;
    [SerializeField] float duratation;

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Fruit"))
        {
            col.GetComponent<SphereCollider>().enabled = false;
            col.GetComponentInChildren<MeshRenderer>().enabled = false;
            switch (col.name)
            {
                case "Aardbei":
                    {
                        PacmanMovement pacmanMovement = col.GetComponent<PacmanMovement>();
                        StartCoroutine(Speed((pacmanMovement),col));
                    }
                    break;
                case "Appel":
                    {
                       
                    }
                    break;
                case "Kers":
                    {
                        playerhealth Playerhealth = FindObjectOfType<playerhealth>();
                        Playerhealth.health++;
                        Destroy(col);
                    }
                    break;
                case "Meloen":
                    {
                        Movement GhostMovement = gameObject.GetComponent<Movement>();
                        GhostMovement.reversecontrols = true;
                        StartCoroutine(Resett((GhostMovement), col));
                    }
                    break;
                case "Sinasappel":
                    {
                        StartCoroutine(Programma(col));
                    }
                    break;
            }
        }
    }
    #region Appel
    private IEnumerator Speed(PacmanMovement pacmanMovement,Collider col)
    {
        pacmanMovement.Speed.x++;
        pacmanMovement.Speed.z++;
        yield return new WaitForSeconds(duratation);
        pacmanMovement.Speed.x--;
        pacmanMovement.Speed.z--;
        Destroy(col);
    }
    #endregion
    #region Meloen
    private IEnumerator Resett(Movement GhostMovement, Collider col)
    {
        yield return new WaitForSeconds(duratation);
        GhostMovement.reversecontrols = false;
        Destroy(col);
    }
    #endregion
    #region Sinasappel
    private IEnumerator Programma(Collider col)
    {
        Switches(0f, 0,col);
        yield return new WaitForSeconds((float)8 / 9 * duratation);
        while (duratation - ((float)8 / 9 * duratation) > 0)
        {
            Switches(0f, 0, col);
            yield return new WaitForSeconds(0.25f);
            Switches(0.5f, 0,col);
            duratation--;
            yield return new WaitForSeconds(0.25f);
        }
        Switches(1f, 1,col);
    }
    private void Switches(float transparancy, int matrialuse, Collider col)
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
                Destroy(col);
            }
            renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, transparancy);
        }
    }
    #endregion
}
