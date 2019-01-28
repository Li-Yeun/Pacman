using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WalkThroughWalls : NetworkBehaviour {

    [SerializeField] float GhostWalkingDuration = 5f;
    [SerializeField] float GhostWalkingCDDuration = 15f;

    public AudioSource poweruppickupsound;
    public bool GhostWalking = false;
    public bool GhostWalkingCD = false;
    void Start()
    { poweruppickupsound = GetComponent<AudioSource>(); }
    void Update()
    {
        if (!hasAuthority)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && !GhostWalkingCD && gameObject.GetComponentInParent<Movement>().name == "Pink")
        {
            poweruppickupsound.Play();
            CmdActivateWalkThroughWallsParticles();
            GetComponentInParent<Invisibiltyy>().Invis = false;
            GetComponentInParent<Invisibiltyy>().CmdInvis();
            GhostWalking = true;
            GhostWalkingCD = true;
            Invoke("StopGhostWalking", GhostWalkingDuration);
            Invoke("CDduration", GhostWalkingCDDuration);
            wallsTimer timerAnimation = FindObjectOfType<wallsTimer>();
            timerAnimation.WallsTimer();
        }
    }

    [CommandAttribute]
    private void CmdActivateWalkThroughWallsParticles()
    {
        RpcActivateWalkThroughWallsParticles();
    }
    [ClientRpcAttribute]
    private void RpcActivateWalkThroughWallsParticles()
    {
        ParticleSystem[] Particles = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in Particles)
        {
            particle.Play();
        }
    }
    [CommandAttribute]
    private void CmdDeactivateWalkThroughWallsParticles()
    {
        RpcDeactivateWalkThroughWallsParticles();
    }
    [ClientRpcAttribute]
    private void RpcDeactivateWalkThroughWallsParticles()
    {
        ParticleSystem[] Particles = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in Particles)
        {
            particle.Stop();
        }
    }

    private void StopGhostWalking()
    {
        GetComponentInParent<Invisibiltyy>().Invis = true;
        GetComponentInParent<Invisibiltyy>().CmdInvis();
        Reset();
    }
    private void CDduration()
    {
        GhostWalkingCD = false;
    }

    public void Reset()
    {
        GhostWalking = false;
        CmdDeactivateWalkThroughWallsParticles();
    }
}
