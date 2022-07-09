using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : SingletonMono<InGameManager>
{
    private bool BulitCollision;
    public List<GameObject> Bulits= new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BulitUpGrade(GameObject ThisBulit)
    {
        if(BulitCollision)
        {
            Instantiate(Bulits[ThisBulit.GetComponent<Bulit>().BulitClass], ThisBulit.transform.position, ThisBulit.transform.rotation);
        }
    }
}
