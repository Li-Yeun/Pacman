using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

// Todo verander de y = 2 naar iets anders.

/// <summary>
/// Deze classe zorgt ervoor dat pacman kan (meermaals) kan springen.
/// 
/// We hadden wat problemen met het overzetten van cordinaten na animaties omdat de cordinaten terug worden gezet elke keer dat je de animatie speelt
/// en hier geen dingen voor zijn binnnen unity.
/// Om dit toch te kunnen destroyen we telkens het gameobject en maken we een nieuwe aan en wordt pacman overgezet in het nieuwe gameobject.
/// Inefficient misschien, doet het z'n werk -> ja
/// 
/// De special triggers zorgen ervoor dat Pacman niet in een muur springt of uit het veld springt.
/// 
/// Als je aan deze classe zit vermoord ik je xxTinus
/// Btw Ik weet dat die y = 2 lelijk is. (working on it)
/// </summary>
public class AnimatorScript : NetworkBehaviour {


    // Use this for initialization
    [Header("Animators")]
    [SerializeField] Animator animatorBodyMesh;
    [SerializeField] PacmanMovement Pacman;
    [SerializeField] GameObject PacmanAnimationObject;


    [Header("Jump Ookay")] 
    [SerializeField] SpecialTrigger2 Jumper;
    [SerializeField] SpecialTrigger2 JumperOuterWalls;
    [SerializeField] SpecialTrigger2 TeleporterClose;
    [SerializeField] float JumpCooldown = 15f;

    private Transform PacmanParentParent;
    private Animator animator2;
    private GameObject go;
    private Light JumpLight;
    private bool AnimationPlaying = false, Jumping = false;

    void Start()
    {
        Light[] PacmanLights = GetComponentsInChildren<Light>();
        foreach (Light light in PacmanLights)
        {
            if (light.name == "Jump Light")
                JumpLight = light;
        }

        Overzetten();
    }

    void Update()
    {
        if (!hasAuthority)
            return;
        if (Input.GetKeyDown(KeyCode.Space) && !Jumping)
        {
            if (!Jumper.Collision && !JumperOuterWalls.Collision && !TeleporterClose.Collision && !AnimationPlaying)
            {
                Jumping = true;
                CmdJump();
                Invoke("Resett", 15f);
            }
        }
    }

    private void Overzetten()
    {
        go = Instantiate(PacmanAnimationObject);
        animator2 = go.GetComponent<Animator>();
        go.transform.parent = GameObject.FindGameObjectWithTag("Object Parent").transform;
        Pacman.transform.parent = go.transform.transform;
    }

    private void Resett()
    {
        Jumping = false;
    }

    private void StartAnimation(float duratation, bool LockMovements)
    {
        AnimationPlaying = true;
        if (LockMovements)
            Pacman.AnimationLock = true;
        Invoke("EndAnimation", duratation);
        if (JumpLight != null)
            JumpLight.enabled = true;
    }

    public void EndAnimation()
    {
        Pacman.transform.parent = PacmanParentParent;
        Destroy(go);
        Overzetten();
        Pacman.Position = new Vector3(Pacman.Position.x, 2, Pacman.Position.z);
        Pacman.AnimationLock = false;
        AnimationPlaying = false;
        if(JumpLight != null)
            JumpLight.enabled = false;
    }

    [CommandAttribute]
    private void CmdJump()
    {
        RpcJump();
    }

    [ClientRpcAttribute]
    private void RpcJump()
    {
        if (FindObjectsOfType<timeranimation>().Length == 1)
        {
            timeranimation timerAnimation = FindObjectOfType<timeranimation>();
            timerAnimation.JumpTimer();
        }

        StartAnimation(1f, true);
        switch (Pacman.currentDirection)
        {
            case 0: animator2.Play("PacmanAnimationJump3"); break;
            case 1: animator2.Play("PacmanAnimationJump"); break;
            case 2: animator2.Play("PacmanAnimationJump4"); break;
            case 3: animator2.Play("PacmanAnimationJump2"); break;
        }
        animatorBodyMesh.Play("PacmanSalto");
    }
}