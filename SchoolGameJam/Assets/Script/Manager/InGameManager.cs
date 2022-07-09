using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : SingletonMono<InGameManager>
{
    private int BulitCount;
    public List<GameObject> Bulits = new List<GameObject>();
    private bool BulitCollision;
    public int _BulitCount {
        get
        {
            return BulitCount;
        }
        set
        {
            BulitCount = value;
        }
    }
    public int MaxBulitCount;
    public void BulitUpGrade(GameObject ThisBulit)
    {
        Debug.Log(ThisBulit.GetComponent<Bulit>().BulitClass);
        if (BulitCollision)
        {
            Debug.Log(ThisBulit.GetComponent<Bulit>().BulitClass + 1);
            Instantiate(Bulits[ThisBulit.GetComponent<Bulit>().BulitClass +1], ThisBulit.transform.position, ThisBulit.transform.rotation);
            BulitCount--;
            BulitCollision = false;
        }
        else
        {
            BulitCollision = true;
        }
    }
}
