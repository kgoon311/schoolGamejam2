using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;

    private void Awake()
    {
        transform.position = new Vector3(1, 1, 1);
    }
    public void OnClickStopButton()
    {
        PausePanel.transform.DOLocalMove(Vector3.zero, 1.5f).SetEase(Ease.OutBack);
        GameManager.In.timeScale = 0;
    }

    public void OnClickResumeButton()
    {
        PausePanel.transform.DOLocalMove(new Vector3(0, 1831, 0), 1.5f).SetEase(Ease.OutBack);
        GameManager.In.timeScale = 1;
    }

    public void OnClickResetButton()
    {
        GameManager.In.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void OnClickHomeButton()
    {
        GameManager.In.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
