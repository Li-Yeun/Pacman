using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class aboutmenu : MonoBehaviour {

    public Button Back;
    public GameObject About;
    public int lengte;
    public int breedte;
    public Button video;
    public GameObject Video;

    public void Setlengte(int lengte)
    { lengte = lengte; }

    public void Setbreedte(int breedte)
    { breedte = breedte; }

    public void Setresolution()
    {
        Screen.SetResolution(lengte, breedte, false);
    }

    public void back()
    {
        About.SetActive(false);
    }

    public void Videomenu()
    { Video.SetActive(true); }
}
