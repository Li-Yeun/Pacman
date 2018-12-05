using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class aboutmenu : MonoBehaviour {

    public Button Back;
    public GameObject About;

    public void back()
    {
        About.SetActive(false);
    }
}
