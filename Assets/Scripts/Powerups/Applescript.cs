using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Applescript : MonoBehaviour {

    public void PacmanInstantiated()
    {
        AnimatorScript animatorScript = FindObjectOfType<AnimatorScript>();
        animatorScript.Apple.Add(gameObject);
    }
}
