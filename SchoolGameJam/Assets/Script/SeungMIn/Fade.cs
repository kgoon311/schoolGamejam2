using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : MonoBehaviour
{
    Image Img;

    private void Awake()
    {
        Img = GetComponent<Image>();
    }
    public void FadeIn(int time)
    {
        Img.DOFade(0, time);
    }

    public void FadeOut(int time)
    {
        Img.DOFade(1, time);
    }
}
