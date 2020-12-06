using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;

    Vector2 direction;
    float enemySpeed = 4f;
    float distanceFromPlayer = 0f;

    public Transform enemyFirePoint;
    public GameObject enemyBulletPrefab;
    public float enemyBulletSpeed = 10f;
    float waitTime = 0f;
    bool move = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Hero(Clone)");
        distanceFromPlayer = Random.Range(4f, 7f);
        GetComponent<EnemyHealthManager>().health = 4;
        GetComponent<EnemyHealthManager>().pointValue = 3;
    }

    void FixedUpdate()
    {
        //if the enemy isn't within a certain range, move forward
        if (move)
        {
            if ((transform.position.x - player.transform.position.x) > distanceFromPlayer + .5f || (transform.position.x - player.transform.position.x) < distanceFromPlayer - .5f)
            {
                direction.x = ((player.transform.position.x - transform.position.x) + distanceFromPlayer) / Mathf.Abs((player.transform.position.x - transform.position.x) + distanceFromPlayer);
            }
            else direction.x = 0f;
            //adjust y-position to be near player
            if (Mathf.Abs(rb.position.y - player.transform.position.y) > .5f)
            {
                direction.y = (player.transform.position.y - transform.position.y) / Mathf.Abs(player.transform.position.y - transform.position.y);
            }
            else direction.y = 0f;
            rb.MovePosition(rb.position + (direction * enemySpeed * Time.fixedDeltaTime));

            //keep moving if x is not within (distanceFromPlayer +- .5) away in X-direction and wihtin .5 of Y value
            move = (transform.position.x - player.transform.position.x) > distanceFromPlayer + .5f || (transform.position.x - player.transform.position.x) < distanceFromPlayer - .5f || Mathf.Abs(transform.position.y - player.transform.position.y) > .5f;
        }
        else
        {
            //wait at current position for a certain amount of time, then shoot
            waitTime += Time.fixedDeltaTime;
            if(waitTime > 1f)
            {
                waitTime = 0f;
                distanceFromPlayer = Random.Range(3f, 7f);
                enemyShoot();
                move = true;
            }
        }
    }

    void enemyShoot()
    {
        //create bullet
        GameObject bullet = Instantiate(enemyBulletPrefab, enemyFirePoint.position, enemyFirePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(enemyFirePoint.right * enemyBulletSpeed * -1, ForceMode2D.Impulse);
    }
}