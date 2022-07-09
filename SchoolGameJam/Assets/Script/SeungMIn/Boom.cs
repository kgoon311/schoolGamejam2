using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.position += (Vector3.down * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Destroy")
        {
            Destroy(gameObject);
        }
    }
}
