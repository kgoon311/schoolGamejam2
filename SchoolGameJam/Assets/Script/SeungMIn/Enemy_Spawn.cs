using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    public GameObject[] enemyObj;
    public Transform[] spawnPoint = new Transform[2];

    public float maxSpawnDelay;
    public float curSpawnDelay;
    private float speedup = 0;
    private void Start()
    {
        Invoke("SpeedUp", 10);
    }
    void Update()
    {
        curSpawnDelay += Time.deltaTime * GameManager.In.timeScale;

        if (curSpawnDelay > maxSpawnDelay)
        {
            spawnEnemy();
            curSpawnDelay = 0;
        }
    }
    void SpeedUp()
    {
        if(maxSpawnDelay >2)
            maxSpawnDelay -= 1;
        speedup+=0.5f;
        Invoke("SpeedUp", 10);
    }
    void spawnEnemy()
    {
        int ranEnemy = Random.Range(0, 1);
        int ranPoint = Random.Range(0, 5);
        GameObject enemyObjcet = Instantiate(enemyObj[ranEnemy], spawnPoint[ranPoint].position, spawnPoint[ranPoint].rotation);
        enemyObjcet.GetComponent<Enemy>().speed += speedup;
    }
}
