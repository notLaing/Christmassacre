using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_Right : MonoBehaviour
{
    public GameObject penguinPrefab;
    public GameObject shootingEnemyPrefab;
    public GameObject chargingEnemyPrefab;
    public GameObject gingerbreadPrefab;
    public GameObject presentRed;
    public GameObject presentBlue;
    public GameObject presentPurple;
    public GameObject presentGreen;
    public GameObject ornamentRed;
    public GameObject ornamentGreen;
    public GameObject ornamentStriped;
    public GameObject cookies;
    GameObject[] enemyArray = new GameObject[12];
    float timeElapsed = -3f;
    float randTime;
    Vector3 displacement;
    int enemySelector = 0;
    public bool spawn = false;

    void Start()
    {
        enemyArray[0] = penguinPrefab;
        enemyArray[1] = shootingEnemyPrefab;
        enemyArray[2] = chargingEnemyPrefab;
        enemyArray[3] = gingerbreadPrefab;
        enemyArray[4] = presentRed;
        enemyArray[5] = presentBlue;
        enemyArray[6] = presentPurple;
        enemyArray[7] = presentGreen;
        enemyArray[8] = ornamentRed;
        enemyArray[9] = ornamentGreen;
        enemyArray[10] = ornamentStriped;
        enemyArray[11] = cookies;
        spawn = true;
        randTime = Random.Range(1.5f, 2.5f);
    }

    void FixedUpdate()
    {
        timeElapsed += Time.fixedDeltaTime;
        //spawn an enemy (or powerup) every random number of seconds after the initial wait
        if(spawn && timeElapsed > randTime)
        {
            timeElapsed = 0f;
            randTime = Random.Range(1.5f, 2.5f);
            enemySelector = Random.Range(0, 7);
            displacement = new Vector3(0f, Random.Range(-3.7f, 4.2f), 0f);

            //create object
            if (enemySelector == 3)
            {
                displacement = new Vector3(Random.Range(-7f, 0f), 0f, 0f);
                Instantiate(enemyArray[enemySelector], new Vector3(4.5f, 7f, 0f) + displacement, transform.rotation);
            }
            else
            {
                //adjust for the multiple instances of presents/ornaments
                switch(enemySelector)
                {
                    case 4:
                        enemySelector = Random.Range(4, 8);
                        break;
                    case 5:
                        enemySelector = Random.Range(8, 11);
                        break;
                    case 6:
                        enemySelector = 11;
                        break;
                    default:
                        break;
                }

                Instantiate(enemyArray[enemySelector], transform.position + displacement, transform.rotation);
            }
        }
    }
}
