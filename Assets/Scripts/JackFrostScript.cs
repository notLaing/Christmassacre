using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackFrostScript : MonoBehaviour
{
    public GameObject player;

    Vector2 direction;
    float enemySpeed = 4f;

    public Transform enemyFirePoint;
    public GameObject enemyBulletPrefab;
    public GameObject iceSpikePrefab;
    public float enemyBulletSpeed = 10f;
    float waitTime = 0f;
    float waitTimeCap = 2f;
    float destinationX;
    int action;
    bool initialMove = true;
    bool move = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Hero(Clone)");
        GetComponent<EnemyHealthManager>().health = 100;
        GetComponent<EnemyHealthManager>().pointValue = 100;
        transform.position = new Vector3(transform.position.x, 0f, 0f);
        destinationX = Random.Range(2f, 7.5f);
    }

    void FixedUpdate()
    {
        //kill Jack
        if(GetComponent<EnemyHealthManager>().health < 1)
        {
            player.GetComponent<PlayerController>().beatJack = true;
        }

        //initial movement
        if (initialMove)
        {
            transform.position -= new Vector3(Time.fixedDeltaTime ,0f, 0f);
            if (!(transform.position.x > 6.85f)) initialMove = false;
        }
        else //try to keep Jack Frost within X: [2, 7.5] and Y: [-2.8, 3.7]
        {
            //Jack Frost moves if not within a certain range of the player's Y and his own predetermined destinationX
            if(move)
            {
                //move X towards destinationX
                if ((transform.position.x - destinationX) > .5f || (transform.position.x - destinationX) < -.5f) direction.x = ((destinationX - transform.position.x)) / Mathf.Abs((destinationX - transform.position.x));
                else direction.x = 0f;

                //adjust y-position to be near player
                if (Mathf.Abs(transform.position.y - player.transform.position.y) > .5f) direction.y = (player.transform.position.y - transform.position.y) / Mathf.Abs(player.transform.position.y - transform.position.y);
                else direction.y = 0f;

                //move Jack Frost
                transform.position += new Vector3(direction.x * enemySpeed * Time.fixedDeltaTime, direction.y * enemySpeed * Time.fixedDeltaTime, 0f);
                
                //keep moving if x is not within (distanceFromPlayer +- .5) away in X-direction and wihtin .5 of Y value
                move = (transform.position.x - destinationX) > .5f || (transform.position.x - destinationX) < -.5f || Mathf.Abs(transform.position.y - player.transform.position.y) > .5f;
            }//if(move)
            else
            {
                //wait at current position for a certain amount of time, then shoot
                waitTime += Time.fixedDeltaTime;
                if (waitTime > waitTimeCap)
                {
                    waitTime = 0f;
                    move = true;
                    switch(Random.Range(0, 6))
                    {
                        case 0:
                            triShot();
                            break;
                        case 1:
                            iceSpike();
                            break;
                        default:
                            enemyShoot();
                            break;
                    }
                }
            }//not moving; action
        }//else

        if(GetComponent<EnemyHealthManager>().health < 50)
        {
            waitTimeCap = 1.5f;
            enemySpeed = 4.5f;
        }
    }

    void enemyShoot()
    {
        //create bullet
        GameObject bullet = Instantiate(enemyBulletPrefab, enemyFirePoint.position, enemyFirePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(enemyFirePoint.right * enemyBulletSpeed * -1, ForceMode2D.Impulse);
    }

    void triShot()
    {
        //create 3 bullets
        for (int i = -1; i < 2; ++i)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, enemyFirePoint.position + new Vector3(0f, (float)i, 0f), enemyFirePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(enemyFirePoint.right * enemyBulletSpeed * -1, ForceMode2D.Impulse);
        }
    }

    void iceSpike()
    {
        Instantiate(iceSpikePrefab);
    }
}
