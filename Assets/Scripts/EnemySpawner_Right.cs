using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_Right : MonoBehaviour
{
    public GameObject basicEnemyPrefab;
    public GameObject shootingEnemyPrefab;
    GameObject[] enemyArray = new GameObject[2];
    float timeElapsed = 0f;
    Vector3 displacement;
    int enemySelector = 0;

    void Start()
    {
        enemyArray[0] = basicEnemyPrefab;
        enemyArray[1] = shootingEnemyPrefab;
    }

    void FixedUpdate()
    {
        timeElapsed += Time.fixedDeltaTime;
        //spawn an enemy every 3 seconds
        if(timeElapsed > 3f)
        {
            timeElapsed = 0f;
            enemySelector = Random.Range(0, 2);
            displacement = new Vector3(0f, Random.Range(-5f, 5f), 0f);
            //create enemy
            GameObject bullet = Instantiate(enemyArray[enemySelector], transform.position + displacement, transform.rotation);
        }
    }
}
