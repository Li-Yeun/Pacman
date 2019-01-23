using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Invisibiltyy : NetworkBehaviour
{
    bool Invis = false;
    Light[] lights;
    GhostStates[] ghostStates;
    public void Start()
    {
        ghostStates = GetComponentsInChildren<GhostStates>();
        lights = GetComponentsInChildren<Light>();
    }
    void Update()
    {
        if (!hasAuthority)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponentInChildren<ParticleSystem>().Play();
            Invoke("Invisible", 0.7f);
            
        }

        if (Invis)
        {
            FindObjectOfType<HUD>().invisibileT.SetActive(true);
            FindObjectOfType<HUD>().invisibileF.SetActive(false);
        }
        else
        {
            FindObjectOfType<HUD>().invisibileT.SetActive(false);
            FindObjectOfType<HUD>().invisibileF.SetActive(true);
        }
    }

    private void Invisible()
    {
        CmdInvis();
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
        
        foreach (GhostStates ghost in ghostStates)
        {
            if (!Invis)
            {
                if (ghost.gameObject.CompareTag("3Dview"))
                {
                    SetLayerRecursively(ghost.gameObject, 11);
                }
                else
                {
                    SetLayerRecursively(ghost.gameObject, 12);
                }
            }
            else
            {
                SetLayerRecursively(ghost.gameObject, 20);
            }
        }
        foreach (Light light in lights)
        {
            light.enabled = !Invis;
        }
    }
    public void Reset()
    {
        Invis = false;
        CmdInvis();
    }
    public static void SetLayerRecursively(GameObject go, int layerNumber)
    {
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
