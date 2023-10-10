using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour

{

    public GameObject enemy;
    float randX;
    Vector2 whereToSpawn;
    public float spawnRate = 5f;
    public float delayBtweenSpawn = 2f;
    float nextSpawn = 0.0f;
    void Update()

    {

        if (Time.time > nextSpawn)

        {
            nextSpawn = Time.time + spawnRate * delayBtweenSpawn;
            randX = Random.Range(-20f, 15f);
            whereToSpawn = new Vector2(randX, transform.position.y);
            Instantiate(enemy, whereToSpawn, Quaternion.identity);
        }
    }
} 
