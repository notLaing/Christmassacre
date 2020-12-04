using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_Right : MonoBehaviour
{
    public GameObject basicEnemyPrefab;
    public GameObject shootingEnemyPrefab;
    public GameObject chargingEnemyPrefab;
    GameObject[] enemyArray = new GameObject[3];
    float timeElapsed = 0f;
    Vector3 displacement;
    int enemySelector = 0;
    public bool spawn = false;

    void Start()
    {
        enemyArray[0] = basicEnemyPrefab;
        enemyArray[1] = shootingEnemyPrefab;
        enemyArray[2] = chargingEnemyPrefab;
        spawn = true;
    }

    void FixedUpdate()
    {
        timeElapsed += Time.fixedDeltaTime;
        //spawn an enemy every 3 seconds
        if(spawn && timeElapsed > 3f)
        {
            timeElapsed = 0f;
            enemySelector = Random.Range(0, 3);
            displacement = new Vector3(0f, Random.Range(-5f, 5f), 0f);
            //create enemy
            GameObject enemy = Instantiate(enemyArray[enemySelector], transform.position + displacement, transform.rotation);
            /*enemySelector++;
            if (enemySelector > 2) enemySelector = 0;*/
        }
    }
}
