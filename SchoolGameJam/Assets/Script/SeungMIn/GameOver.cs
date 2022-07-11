using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class End : MonoBehaviour
{
    Image Img;

    private void Awake()
    {
        Img = GetComponent<Image>();
    }
    public void FadeInOut(float fadevalue, int time)
    {
        Img.DOFade(fadevalue, time);
    }

    void GameOver()
    {
        FadeInOut(0.7f, 1);
    }
}
