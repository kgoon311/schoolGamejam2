using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulit : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private int hp;
    private int _Hp
    {
        get { return hp; }
        set
        {
            //HpBar 조절
            hp = value;
            if (hp <= 0)
            {
                InGameManager.In._BulitCount--;
                Destroy(gameObject);
            }
        }
    }

    [SerializeField]
    private int damege;
    [SerializeField]
    private int speed;
    [SerializeField]
    private int wallHitDamege;
    [SerializeField]
    private int monsterHitDamege;
    [SerializeField]
    private Vector3 directPos;//진행 방향

    public int bulitClass;
    private CircleCollider2D circleCollider2D;
    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        directPos = new Vector3(transform.rotation.x,transform.rotation.y) * 100;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        transform.position += directPos * speed * Time.deltaTime;
    }
    private void FixedUpdate()
    {
        RaycastHit2D[] CastObjects = Physics2D.CircleCastAll(transform.position, circleCollider2D.radius, Vector2.zero, 0, LayerMask.GetMask("Bullet"));
        for (int i = 0; CastObjects.Length > i; i++)
        {
            if (CastObjects[i].transform.GetComponent<Bulit>().bulitClass == bulitClass
                && bulitClass < 5 && CastObjects[i].transform.gameObject != gameObject)
            {
                InGameManager.In.BulletUpgrade(gameObject,directPos);
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
            _Hp -= wallHitDamege;
            Bounce(collision);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Bounce(collision);
            SoundManager.In.PlaySound("j", SoundType.SFX, 1, 1);
            _Hp -= monsterHitDamege;
            StartCoroutine(collision.gameObject.GetComponent<Enemy>().OnHit(damege));
        }
    }
    private void Bounce(Collision2D collision)
    {
        Vector3 incldentVector = directPos;

        Vector3 normalVector = collision.contacts[0].normal;//법선 위치;

        directPos = Vector3.Reflect(incldentVector, normalVector);
    }
}
