using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dice
{
    public int _DiceIdx;
    public List<Sprite> DotSum = new List<Sprite>();
    public List<int> Dotidx = new List<int>();
}
public class Player : MonoBehaviour
{
    private LineRenderer blastline;
    private Vector3 mousePos;
    private Vector3 clickPos;


    [Header("Dice")]
    [SerializeField]
    public List<Dice> DiceImages = new List<Dice>();
    [SerializeField]
    private Image MainDice;
    [SerializeField]
    private Image[] ServeDice = new Image[2];
    [Header("Mark")]
    [SerializeField]
    private GameObject markObject;
    [SerializeField]
    private int markCount;
    [SerializeField]
    private float markDis;
    private List<GameObject> markGroup = new List<GameObject>();

    private int[] DiceArrey = { 0, 1, 2 };
    private int Diceidx;
    private int RandomDot;

    private bool StopShot;
    void Start()
    {
        blastline = GetComponent<LineRenderer>();
        for (int i = 0; i < 3; i++)
        {
            DiceImages[i].DotSum.AddRange(Resources.LoadAll<Sprite>($"Dice/{i + 1}Dice/"));
        }
        for (int i = 0; i < markCount; i++)
        {
            markGroup.Add(Instantiate(markObject));
            markGroup[i].SetActive(false);
        }
    }

    void Update()
    {
        DrawLine();
    }

    void DrawLine()
    {
        if (StopShot == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clickPos.z = 0;

                blastline.SetPosition(0, clickPos);
                blastline.SetPosition(1, clickPos);

                for (int i = 0; i < markCount; i++)
                    markGroup[i].SetActive(true);
            }
            else if (Input.GetMouseButton(0))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;

                blastline.SetPosition(1, mousePos);
                for (int i = 0; i < markCount; i++)
                    MarkRoute(i);

            }
            else if (Input.GetMouseButtonUp(0))
            {
                blastline.SetPosition(0, clickPos);
                blastline.SetPosition(1, clickPos);

                if (InGameManager.In.MaxBulitCount > InGameManager.In._BulitCount)
                    StartCoroutine(Shot());

                for (int i = 0; i < markCount; i++)
                    markGroup[i].SetActive(false);
            }
        }
    }
    private void MarkRoute(float count)
    {
        markGroup[(int)count].transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f) //�߻� ��ġ
            + (Vector2.ClampMagnitude(clickPos - mousePos, markDis)) * count;//�߻� ���� / for�� idx
    }
    public void ChanegeDice(int idx)
    {
        Sprite SaveDice = MainDice.sprite;
        if ((idx == 0|| Input.GetKeyDown(KeyCode.Alpha1)) && InGameManager.In.FirstDiceUnLock == true)
        {
            MainDice.sprite = ServeDice[0].sprite;
            ServeDice[0].sprite = SaveDice;
            int SaveDot = DiceArrey[0];
            DiceArrey[0] = DiceArrey[1];
            DiceArrey[1] = SaveDot;
            Diceidx = DiceArrey[0];
        }
        else if (idx ==1|| Input.GetKeyDown(KeyCode.Alpha2) && InGameManager.In.SecoundDiceUnLock == true)
        {
            MainDice.sprite = ServeDice[1].sprite;
            ServeDice[1].sprite = SaveDice;
            int SaveDot = DiceArrey[0];
            DiceArrey[0] = DiceArrey[2];
            DiceArrey[2] = SaveDot;
            Diceidx = DiceArrey[0];
        }
    }
    private IEnumerator Shot()
    {
        StopShot = true;
        InGameManager.In._BulitCount++;

        Vector3 vec = clickPos - mousePos; 
        Quaternion Mouserotate = Quaternion.Euler(vec);


        RandomDot = Random.Range(0, DiceImages[Diceidx].DotSum.Count);
        RandomDot = Random.Range(0, DiceImages[Diceidx].DotSum.Count);

        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime * 3;
            MainDice.gameObject.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(0, 720, timer), 0);
            yield return null;
        }

        MainDice.sprite = DiceImages[Diceidx].DotSum[RandomDot];

        yield return new WaitForSeconds(0.3f);

        Instantiate(InGameManager.In.Bulits[DiceImages[Diceidx].Dotidx[RandomDot]], transform.position + Vector3.up * 0.5f, Mouserotate);
        StopShot = false;

        yield return null;
    }
}
