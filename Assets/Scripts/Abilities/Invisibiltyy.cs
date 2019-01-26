using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Invisibiltyy : NetworkBehaviour
{
    public bool Invis = false;
    private bool Orange = false , Lock = false;
    private Light[] lights;
    private GhostStates[] ghostStates;
    public void Start()
    {
        ghostStates = GetComponentsInChildren<GhostStates>();
        lights = GetComponentsInChildren<Light>();
        if(GetComponent<Movement>().name == "Orange")
        {
            Orange = true;
        }
    }
    void Update()
    {
        if (!hasAuthority)
            return;
        if (Input.GetKeyDown(KeyCode.Space) && Orange && Lock == false)
        {
            Lock = true;
            CmdActivateInvisibleParticles();
            StartCoroutine(Invisible());   
        }
        if (Orange)
        {
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
    }

    private IEnumerator Invisible()
    {
        yield return new WaitForSeconds(0.7f);
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
        Lock = false;
        foreach (GhostStates ghost in ghostStates)
        {
            if (!Invis)
            {
                if (ghost.gameObject.CompareTag("3Dview") && Orange)
                {
                    SetLayerRecursively(ghost.gameObject, 11);
                }
                else if (!ghost.gameObject.CompareTag("3Dview"))
                {
                    switch (gameObject.GetComponent<Movement>().name)
                    {
                        case "Pink":
                            SetLayerRecursively(ghost.gameObject, 18);
                            break;
                        case "Orange":
                            SetLayerRecursively(ghost.gameObject, 19);
                            break;
                    }

                }
            }
            else
            {
                if (Orange)
                {
                    SetLayerRecursively(ghost.gameObject, 20);
                }
                else if (!ghost.gameObject.CompareTag("3Dview") && !Orange)
                { SetLayerRecursively(ghost.gameObject, 20); }
            }
        }

        if (Orange)
        {
            foreach (Light light in lights)
            {
                light.enabled = !Invis;
            }
        }
    }

    private void SetLayerRecursively(GameObject go, int layerNumber)
    {
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }

    [CommandAttribute]
    private void CmdActivateInvisibleParticles()
    {
        RpcActivateInvisibleParticles();
    }
    [ClientRpcAttribute]
    private void RpcActivateInvisibleParticles()
    {
        GetComponentInChildren<ParticleSystem>().Play();
    }

    public void Reset() 
    {
        foreach (GhostStates ghost in ghostStates)
        {
            if (ghost == null)
                return;
            if (ghost.gameObject.CompareTag("3Dview") && Orange)
            {
                SetLayerRecursively(ghost.gameObject, 11);
            }
            else if (!ghost.gameObject.CompareTag("3Dview"))
            {
                switch (gameObject.GetComponent<Movement>().name)
                {
                    case "Pink":
                        SetLayerRecursively(ghost.gameObject, 18);
                        break;
                    case "Orange":
                        SetLayerRecursively(ghost.gameObject, 19);
                        break;
                }
            }
        }

        if (Orange)
        {
            foreach (Light light in lights)
            {
                light.enabled = true;
            }
        }

        Lock = false;
        Invis = false;
    }
}
