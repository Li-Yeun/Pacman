using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Invisibiltyy : NetworkBehaviour
{
    bool Invis = false;
    GhostStates[] Ghosts;
    Light[] lights;
    public void Start()
    {
        Ghosts = gameObject.GetComponentsInChildren<GhostStates>();
        lights = GetComponentsInChildren<Light>();
    }
    void Update()
    {
        if (!hasAuthority)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdInvis();
        }
    }
    [CommandAttribute]
    public void CmdInvis()
    {
        RpcInvis();
    }
    [ClientRpcAttribute]
    private void RpcInvis()
    {
        Invis = !Invis;
        foreach (GhostStates Ghost in Ghosts)
        {
            Ghost.gameObject.SetActive(!Invis);
        }
        foreach (Light light in lights)
        {
            light.enabled = !Invis;
        }
    }
    public void Reset()
    {
        Invis = true;
        CmdInvis();
    }
}
