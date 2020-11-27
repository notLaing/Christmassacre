using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEnemy : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;

    Vector2 direction;
    float enemySpeed = 1f;
    float timeElapsed = 0f;
    int phase = 0;
    int shuffleCount = 0;

    void Start()
    {
        player = GameObject.Find("Hero(Clone)");
        direction.x = -1f;
        direction.y = 0f;
        GetComponent<EnemyHealthManager>().health = 5;
        GetComponent<EnemyHealthManager>().pointValue = 3;
    }

    void FixedUpdate()
    {
        switch(phase)
        {
            case 0://initial walk/set up
                enemySpeed = 2f;
                if (Mathf.Abs(rb.position.y - player.transform.position.y) > .35f)
                {
                    direction.y = (player.transform.position.y - transform.position.y) / Mathf.Abs(player.transform.position.y - transform.position.y);
                }
                else direction.y = 0f;
                break;

            case 1://shuffle in place, prep for charge
                enemySpeed = 1.5f;
                direction.x = 0f;
                //shuffle up
                if (shuffleCount < 5) direction.y = .4f;
                //shuffle down
                else direction.y = -.4f;
                shuffleCount++;
                shuffleCount %= 10;
                break;

            case 2://charge straight
                enemySpeed = 10f;
                direction.x = -1f;
                direction.y = 0f;
                break;
        }

        timeElapsed += Time.fixedDeltaTime;
        if (timeElapsed > 3.5f) phase = 2;
        else if (timeElapsed > 2f) phase = 1;


        //move
        rb.MovePosition(rb.position + (direction * enemySpeed * Time.fixedDeltaTime));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Player");
        }
        else if (collider.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
