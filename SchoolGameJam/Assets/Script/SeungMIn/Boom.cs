using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.position += (Vector3.down * speed * Time.deltaTime * GameManager.In.timeScale);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Destroy")
        {
            InGameManager.In._HP--;
            Destroy(gameObject);
        }
    }
}
