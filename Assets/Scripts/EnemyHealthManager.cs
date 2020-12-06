using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public GameObject player;
    public int health = 3;
    public int pointValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Hero(Clone)");
    }

    void FixedUpdate()
    {
        //kill
        if (health < 1)
        {
            player.GetComponent<PlayerController>().stageKills += 1;
            GameManagerScript.score += pointValue;
            if (transform.Find("Jack") != null) player.GetComponent<PlayerController>().beatJack = true;
            Destroy(gameObject);
        }
    }
}
