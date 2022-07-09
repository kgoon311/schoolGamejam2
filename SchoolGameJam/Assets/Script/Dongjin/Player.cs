using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dice
{
    public List<Sprite> DotSum = new List<Sprite>();
    public List<int> Dotidx = new List<int>();
}
public class Player : MonoBehaviour
{
    private LineRenderer blastline;
    private Vector3 mousePos;
    [Header("Dice")]
    [SerializeField]
    public List<Dice> DiceImages = new List<Dice>();
    [SerializeField]
    private Image MainDice;
    [SerializeField]
    private Image[] ServeDice = new Image[2];

    private int Diceidx;
    private int RandomDot;

    private bool StopShot;
    void Start()
    {
        blastline = GetComponent<LineRenderer>();
        for (int i = 0; i < 3; i++)
        {
            DiceImages[i].DotSum.AddRange(Resources.LoadAll<Sprite>($"Dice/{i + 1}Dice/"));
            Debug.Log("asdasd");
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
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                blastline.SetPosition(0, transform.position + Vector3.up * 0.5f);
                blastline.SetPosition(1, mousePos);
            }
            else if (Input.GetMouseButton(0))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                blastline.SetPosition(0, transform.position + Vector3.up * 0.5f);
                blastline.SetPosition(1, mousePos);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                blastline.SetPosition(1, transform.position + Vector3.up * 0.5f);
                if (InGameManager.In.MaxBulitCount > InGameManager.In._BulitCount++)
                    StartCoroutine(Shot());
            }
        }
    }
    
    private IEnumerator Shot()
    {
        StopShot = true;
        Quaternion Mouserotate = Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(mousePos.normalized.y, mousePos.normalized.x) + 90));
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
