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
    Animator animator2;
    [SerializeField] Animator animatorBodyMesh;
    [SerializeField] PacmanMovement Pacman;
    [SerializeField] GameObject PacmanAnimationObject;
    private Transform PacmanParentParent; // was een empty serielizedfield

    GameObject go;
    [Header("Jump Ookay")] 
    [SerializeField] SpecialTrigger2 Jumper;
    [SerializeField] SpecialTrigger2 JumperOuterWalls;
    [SerializeField] SpecialTrigger2 TeleporterClose;

    Light JumpLight;
    public bool AnimationPlaying = false;

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
    public void Overzetten()
    {
        go = Instantiate(PacmanAnimationObject);
        animator2 = go.GetComponent<Animator>();
        go.transform.parent = GameObject.FindGameObjectWithTag("Object Parent").transform;
        Pacman.transform.parent = go.transform.transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!Jumper.Collision && !JumperOuterWalls.Collision && !TeleporterClose.Collision && !AnimationPlaying)
            {
                CmdJump();
            }
        }
    }

    void StartAnimation(float duratation, bool LockMovements)
    {
        AnimationPlaying = true;
        if (LockMovements)
            Pacman.AnimationLock = true;
        Invoke("EndAnimation", duratation);
        JumpLight.enabled = true;
    }

    void EndAnimation()
    {
        Pacman.transform.parent = PacmanParentParent;
        Destroy(go);
        Overzetten();
        Pacman.Position = new Vector3(Pacman.Position.x, 2, Pacman.Position.z);
        Pacman.AnimationLock = false;
        AnimationPlaying = false;
        JumpLight.enabled = false;
    }

    [CommandAttribute]
    public void CmdJump()
    {
        RpcJump();
    }

    [ClientRpcAttribute]
    public void RpcJump()
    {
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