using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int hp;
    public Sprite[] sprites;
    public GameObject[] attackObj;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position += (Vector3.down * speed * Time.deltaTime);
    }

    void OnHit(int dmg)
    {
        hp -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);

        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Destroy")
        {
            Destroy(gameObject);
        }else if(collision.gameObject.tag == "Attack")
        {
            StartCoroutine( Boom());
        }
    }
    IEnumerator Boom()
    {
        speed = 0;
        int ranAttack = Random.Range(0, 1);
        Instantiate(attackObj[ranAttack], transform.position, transform.rotation);
        yield return new WaitForSeconds(1f);
        speed = 1;
    }
}
