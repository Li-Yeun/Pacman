using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MeloenPowerup : NetworkBehaviour
{
    public AudioSource poweruppickupsound;

    void Start()
    { poweruppickupsound = GetComponent<AudioSource>(); }

    [SerializeField] float duration = 15f;

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
        PlayerOnline[] Players = FindObjectsOfType<PlayerOnline>();
        foreach (PlayerOnline player in Players)
        {
            player.AddToGridList((int)gameObject.transform.localPosition.x, (int)gameObject.transform.localPosition.z);
        }
    }

    private IEnumerator Resett(Movement movement)
    {
        yield return new WaitForSeconds(duration);
        movement.reversecontrols = false;
        Destroy(gameObject);
    }

    public void Reset()
    {
        Destroy(gameObject);
    }
}