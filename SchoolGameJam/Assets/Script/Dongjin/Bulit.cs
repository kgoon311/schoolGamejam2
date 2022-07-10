using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulit : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private int Hp;
    private int _Hp
    {
        get { return Hp; }
        set
        {
            //HpBar Á¶Àý
            Hp = value;
            if(Hp <= 0)
            {
                InGameManager.In._BulitCount--;
                Destroy(gameObject);
            }
        }
    }

    [SerializeField]
    private int Damege;
    [SerializeField]
    private int Speed;
    [SerializeField]
    private int WallHitDamege;
    [SerializeField]
    private int MonsterHitDamege;

    public int BulitClass;
    private Rigidbody2D rg;
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        rg.AddForce(transform.up*Speed);
    }

    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bulit") && collision.gameObject.GetComponent<Bulit>().BulitClass == BulitClass && BulitClass < 5)
        {
            InGameManager.In.BulitUpGrade(this.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            SoundManager.In.PlaySound("j", SoundType.SE, 1, 1);
           _Hp -= WallHitDamege;
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            SoundManager.In.PlaySound("j", SoundType.SE, 1, 1);
            _Hp -= MonsterHitDamege;
            StartCoroutine(collision.gameObject.GetComponent<Enemy>().OnHit(Damege));
        }
    }
}
