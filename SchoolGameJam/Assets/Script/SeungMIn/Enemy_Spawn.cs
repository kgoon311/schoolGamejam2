using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    public GameObject[] enemyObj;
    public Transform[] spawnPoint = new Transform[2];

    public float maxSpawnDelay;
    public float curSpawnDelay;

    void Update()
    {
        curSpawnDelay += Time.deltaTime * GameManager.In.timeScale;

        if (curSpawnDelay > maxSpawnDelay)
        {
            spawnEnemy();
            maxSpawnDelay = 2/*Random.Range(0.5f, 3f)*/;
            curSpawnDelay = 0;
        }
    }

    void spawnEnemy()
    {
        int ranEnemy = Random.Range(0, 1);
        int ranPoint = Random.Range(0, 5);
        Instantiate(enemyObj[ranEnemy], spawnPoint[ranPoint].position, spawnPoint[ranPoint].rotation);
    }
}
