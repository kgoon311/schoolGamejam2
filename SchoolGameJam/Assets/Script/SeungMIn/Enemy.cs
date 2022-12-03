using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Enemy : MonoBehaviour
{
    public float speed;
    public int hp;
    public bool Booms;
    public Sprite[] sprites;
    public List<GameObject> attackObj = new List<GameObject>();

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position += (Vector3.down * speed * Time.deltaTime * GameManager.In.timeScale);
    }

    public IEnumerator OnHit(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            InGameManager.In._XP += 30;
            InGameManager.In._Score += 700;
            Destroy(gameObject);
        }
        spriteRenderer.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(0.5f);
        if (spriteRenderer != null)spriteRenderer.color =(new Color(255, 255, 255));
        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Destroy")
        {
            Destroy(gameObject);
            InGameManager.In._HP--;
        }
        else if (collision.gameObject.tag == "Attack"&&Booms)
        {
            StartCoroutine(Boom());
        }
    }
    IEnumerator Boom()
    {
        speed = 0;
        yield return new WaitForSeconds(1.5f);
        int ranAttack = Random.Range(0, attackObj.Count);
        Instantiate(attackObj[ranAttack], transform.position, transform.rotation);
        yield return new WaitForSeconds(1f);
        speed = 1;
    }
}
