using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_Right : MonoBehaviour
{
    public GameObject penguinPrefab;
    public GameObject shootingEnemyPrefab;
    public GameObject chargingEnemyPrefab;
    public GameObject gingerbreadPrefab;
    GameObject[] enemyArray = new GameObject[4];
    float timeElapsed = 0f;
    Vector3 displacement;
    int enemySelector = 0;
    public bool spawn = false;

    void Start()
    {
        enemyArray[0] = penguinPrefab;
        enemyArray[1] = shootingEnemyPrefab;
        enemyArray[2] = chargingEnemyPrefab;
        enemyArray[3] = gingerbreadPrefab;
        spawn = true;
    }

    void FixedUpdate()
    {
        timeElapsed += Time.fixedDeltaTime;
        //spawn an enemy every 3 seconds
        if(spawn && timeElapsed > 3f)
        {
            timeElapsed = 0f;
            enemySelector = Random.Range(0, 4);
            displacement = new Vector3(0f, Random.Range(-5f, 5f), 0f);

            //create enemy
            if (enemySelector == 3)
            {
                displacement = new Vector3(Random.Range(-7f, 0f), 0f, 0f);
                Instantiate(enemyArray[enemySelector], new Vector3(4.5f, 7f, 0f) + displacement, transform.rotation);
            }
            else Instantiate(enemyArray[enemySelector], transform.position + displacement, transform.rotation);
        }
    }
}
