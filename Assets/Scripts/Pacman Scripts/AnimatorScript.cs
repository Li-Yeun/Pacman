﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
public class AnimatorScript : MonoBehaviour {


    // Use this for initialization
    Animator animator;
    [SerializeField] PacmanMovement Pacman;
    [SerializeField] GameObject PacmanAnimationObject;
    [SerializeField] Transform PacmanParentParent;
    [SerializeField] Animator animatorSalto;
    GameObject go;
    [Header("Jump Ookay")] 
    [SerializeField] SpecialTrigger2 Jumper;
    [SerializeField] SpecialTrigger2 JumperOuterWalls;
    public bool JumpRunning = false;

    void Start()
    {
        Overzetten();
    }
    public void Overzetten()
    {
        go = Instantiate(PacmanAnimationObject);
        animator = go.GetComponent<Animator>();
        go.transform.parent = PacmanParentParent;
        Pacman.transform.parent = go.transform.transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!Jumper.Collision && !JumperOuterWalls.Collision && !JumpRunning)
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        JumpRunning = true;
        Pacman.AnimationLock = true;
        switch (Pacman.currentDirection)
        {
            case 0: animator.Play("PacmanAnimationJump3"); break; 
            case 1: animator.Play("PacmanAnimationJump");  break;
            case 2: animator.Play("PacmanAnimationJump4"); break;
            case 3: animator.Play("PacmanAnimationJump2"); break;
        }
        animatorSalto.Play("PacmanSalto");
        Invoke("EndJump", 1f);
    }
    void EndJump()
    {
        Pacman.transform.parent = PacmanParentParent;
        Destroy(go);
        Overzetten();
        Pacman.Position = new Vector3(Pacman.Position.x, 2, Pacman.Position.z);
        Pacman.AnimationLock = false;
        JumpRunning = false;
    }
}