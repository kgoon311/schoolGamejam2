using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class InGameManager : Singleton<InGameManager>
{
    [Header("Bulit")]
    public List<GameObject> Bulits = new List<GameObject>();
    private int BulitCount;
    private bool BulitCollision;
    public int MaxBulitCount;
    [Space(1)]
    [Header("Canvas")]
    [SerializeField] private TextMeshProUGUI BulitCountText;
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private Slider XpSlider;
    [SerializeField] private Image[] HpImage;
    [SerializeField] private GameObject[] LockingPanel;
    [SerializeField] private TextMeshProUGUI ScoreText2;

    [SerializeField] private Image FadePanel;
    [SerializeField] private GameObject GameOverObj;
    [Space(1)]
    [Header("Play Related")]
    [SerializeField] int Hp;
    [SerializeField] int Score;
    [SerializeField] int LV;
    [SerializeField] float Xp;
    [Space(1)]
    [Header("Bool")]
    public bool FirstDiceUnLock;
    public bool SecoundDiceUnLock;
    public int _HP
    {
        get { return Hp; }
        set
        {
            SoundManager.In.PlaySound("DMG", SoundType.SFX, 1, 1);
            Hp = value;
            if (Hp <= 0)
            {
                GameOver();
            }
            for (int i = 0; i < 3; i++)
            {
                HpImage[i].color = Color.black;
            }
            for (int i = 0; i < Hp; i++)
            {
                HpImage[i].color = Color.red;
            }
        }
    }
    public int _LV
    {
        get { return LV; }
        set
        {
            SoundManager.In.PlaySound("Level Up", SoundType.SFX, 1, 1);
            LV = value;
            LevelText.text = $"LV.{LV}";
            BulitCountText.text = $"{BulitCount} / {MaxBulitCount}";
            MaxBulitCount += 2;
            if (LV == 5)
            {
                FirstDiceUnLock = true;
                LockingPanel[0].gameObject.SetActive(false);
            }
            if (LV == 10)
            {
                SecoundDiceUnLock = true;
                LockingPanel[1].gameObject.SetActive(false);
            }
        }
    }
    public int _Score
    {
        get { return Score; }
        set
        {
            Score = value;
            ScoreText.text = $"Score : {Score}";
        }
    }
    public float _XP
    {
        get { return Xp; }
        set
        {
            Xp = value;
            while (Xp > 100)
            {
                _LV += 1;
                Xp -= 100;
            }
            BulitCountText.text = $"{BulitCount} / {MaxBulitCount}";
            XpSlider.value = (float)Xp / 100;
        }
    }
    public int _BulitCount
    {
        get => BulitCount;
        set
        {
            BulitCount = value;
            BulitCountText.text = $"{BulitCount} / {MaxBulitCount}";
        }
    }
    private void Start()
    {
        SoundManager.In.PlaySound("InGame", SoundType.BGM, 0.4f, 1);
        BulitCountText.text = $"{BulitCount} / {MaxBulitCount}";
        ScoreText.text = "Score : 0";
        LevelText.text = $"LV.{LV}";
    }
    void GameOver()
    {
        SoundManager.In.PlaySound("Game Over", SoundType.SFX, 1, 1);
        ScoreText2.text = $"Score{_Score}";
        FadePanel.gameObject.SetActive(true);
        GameOverObj.SetActive(true);
        GameManager.In.timeScale = 0;
    }
    public void FadeInOut(float FadeValue, float time)
    {
        FadePanel.DOFade(FadeValue, time);
    }
    public void BulletUpgrade(GameObject ThisBulit, Vector3 direct)
    {
        Debug.Log(direct);
        Instantiate(Bulits[ThisBulit.GetComponent<Bulit>().bulitClass + 1], ThisBulit.transform.position, Quaternion.Euler(direct));
        _BulitCount--;
    }
}
