using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class aboutmenu : MonoBehaviour {

    public Button Back;
    public GameObject About;
    public int width;
    public int height;
    public Button video;
    public GameObject Video;

    public void Setwidth(int Width)
    { width = Width; }

    public void Setheight(int Height)
    { height = Height; }

    public void Setresolution()
    {
        Screen.SetResolution(width, height, false);
    }

    public void back()
    {
        About.SetActive(false);
    }

    public void Videomenu()
    { Video.SetActive(true); }
}
