using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulit : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private int Hp;
    [SerializeField]
    private int Damege;
    [SerializeField]
    private int Speed;

    public int BulitClass;    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Bulit>().BulitClass == this.BulitClass)
        {

        }
    }
}
