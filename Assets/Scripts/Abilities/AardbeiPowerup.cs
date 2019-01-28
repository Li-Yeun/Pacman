using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AardbeiPowerup : NetworkBehaviour
{
    public AudioSource poweruppickupsound;

    void Start()
    { poweruppickupsound = GetComponent<AudioSource> (); }

    [SerializeField] float duration = 15f;

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
                CmdCollision(col.gameObject);
                poweruppickupsound.Play();
                break;
        }
    }

    [CommandAttribute]
    private void CmdCollision(GameObject col)
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