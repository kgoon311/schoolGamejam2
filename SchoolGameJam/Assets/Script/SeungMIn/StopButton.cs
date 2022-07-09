using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StopButton : MonoBehaviour
{
    void Stop()
    {
        transform.DOMove(Vector3.down, 1.5f).SetEase(Ease.OutBack);
    }
}
