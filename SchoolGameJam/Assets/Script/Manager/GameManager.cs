using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : SingletonMono<GameManager>
{
    public float timeScale = 1f;
    [SerializeField] private Image FadePanel;
    public void FadeInOut(float FadeValue,float time)
    {
        FadePanel.DOFade(FadeValue, time);
    }
}