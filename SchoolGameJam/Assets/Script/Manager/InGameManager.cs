using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameManager : SingletonMono<InGameManager>
{
    [Header("Bulit")]
    public List<GameObject> Bulits = new List<GameObject>();
    private int BulitCount;
    private bool BulitCollision;
    [Space(1)]
    [Header("Canvas")]
    [SerializeField] private TextMeshProUGUI BulitCountText;
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private Slider XpSlider;
    [SerializeField] private Image[] HpImage;
    [Space(1)]
    [Header("Play Related")]
    [SerializeField] int Hp;
    [SerializeField] int Score;
    [SerializeField] int LV;
    [SerializeField] int Xp;
    [Space(1)]
    [Header("Bool")]
    public bool FirstDiceUnLock;
    public bool SecoundDiceUnLock;
    public int _HP
    {
        get { return Hp; }
        set {
            Hp = value;
            if (Hp < 0)
            {
                GameOver();
            }
            for(int i = 0;i<3;i++)
            {
                HpImage[i].color = Color.black;
            }
            for(int i = 0; i<Hp;i++)
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
            LV = value;
            LevelText.text =$"LV.{LV}";
            MaxBulitCount += 5;
            if(LV == 5)
            {
                FirstDiceUnLock = true;
            }
            if(LV == 10)
            {
                SecoundDiceUnLock = true;
            }
        }
    }
    public int _Score
    {
        get { return LV; }
        set
        {
            LV = value;
            ScoreText.text =$"Score : {Score}";
        }
    }
    public int _XP
    {
        get { return Xp; }
        set
        {
            Xp = value;
            if(Xp > 100)
            {
                _LV++;
                Xp -= 100;
            }
            XpSlider.value = Xp / 100;
        }
    }
    public int _BulitCount {
        get
        {
            return BulitCount;
        }
        set
        {
            BulitCount = value;
            BulitCountText.text = $"{BulitCount} / {MaxBulitCount}";
        }
    }
    public int MaxBulitCount;
    private void Start()
    {
        BulitCountText.text = $"{BulitCount} / {MaxBulitCount}";
    }
    void GameOver()
    {

    }
    public void BulitUpGrade(GameObject ThisBulit)
    {
        Debug.Log(ThisBulit.GetComponent<Bulit>().BulitClass);
        if (BulitCollision)
        {
            Debug.Log(ThisBulit.GetComponent<Bulit>().BulitClass + 1);
            Instantiate(Bulits[ThisBulit.GetComponent<Bulit>().BulitClass +1], ThisBulit.transform.position, ThisBulit.transform.rotation);
            _BulitCount--;
            BulitCollision = false;
        }
        else
        {
            BulitCollision = true;
        }
    }
}
