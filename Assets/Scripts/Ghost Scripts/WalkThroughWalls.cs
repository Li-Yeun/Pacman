using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WalkThroughWalls : NetworkBehaviour {
    public bool GhostWalking = false;
    public bool GhostWalkingCD = false;
    public float GhostWalkingDuration = 5f;
    public float GhostWalkingCDDuration = 15f;

    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && !GhostWalkingCD && gameObject.GetComponentInParent<Movement>().name == "Pink")
        {
            GetComponentInParent<Invisibiltyy>().CmdInvis();
            ParticleSystem[] Particles = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem particle in Particles)
            {
                particle.Play();
            }
            GhostWalking = true;
            GhostWalkingCD = true;
            Invoke("StopGhostWalking", GhostWalkingDuration);
            Invoke("CDduration", GhostWalkingCDDuration);
            wallsTimer timerAnimation = FindObjectOfType<wallsTimer>();
            timerAnimation.WallsTimer();
        }
    }
 
    private void StopGhostWalking()
    {
        GhostWalking = false;
        ParticleSystem[] Particles = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in Particles)
        {
            particle.Stop();
        }
        GetComponentInParent<Invisibiltyy>().CmdInvis();
    }
    private void CDduration()
    {
        GhostWalkingCD = false;
    }
}
