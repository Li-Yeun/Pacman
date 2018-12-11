using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerOnline : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        if (!isLocalPlayer)
            return;
        CmdSpawnMyPlayer();
	}

    [SerializeField] GameObject PlayerObject;
	// Update is called once per frame
	void Update () {
        return;
	}

    [Command]
    void CmdSpawnMyPlayer()
    {
        GameObject go = Instantiate(PlayerObject);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

}
