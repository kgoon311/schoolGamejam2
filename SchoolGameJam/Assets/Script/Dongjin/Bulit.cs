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
            if (Hp <= 0)
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
    private CircleCollider2D circleCollider2D;
    private Rigidbody2D rg;
    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        rg = GetComponent<Rigidbody2D>();
        rg.AddForce(transform.up * Speed);
    }

    void Update()
    {
        rg.velocity *= 1;
    }
    private void FixedUpdate()
    {
        RaycastHit2D[] CastObjects = Physics2D.CircleCastAll(transform.position, circleCollider2D.radius, Vector2.zero, 0, LayerMask.GetMask("Bullet"));
        for (int i = 0; CastObjects.Length > i; i++)
        {
            if (CastObjects[i].transform.GetComponent<Bulit>().BulitClass == BulitClass
                && BulitClass < 5 && CastObjects[i].transform.gameObject != gameObject)
            {
                InGameManager.In.BulletUpgrade(gameObject);
                Destroy(CastObjects[i].transform.gameObject);
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            SoundManager.In.PlaySound("j", SoundType.SFX, 1, 1);
            _Hp -= WallHitDamege;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SoundManager.In.PlaySound("j", SoundType.SFX, 1, 1);
            _Hp -= MonsterHitDamege;
            StartCoroutine(collision.gameObject.GetComponent<Enemy>().OnHit(Damege));
        }
    }
}
