using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    //ArrayList<GameObject> enemies;
    public GameObject player;
    public Rigidbody2D rb;

    Vector2 direction;
    float enemySpeed = 3f;

    void Start()
    {
        player = GameObject.Find("Hero(Clone)");
    }

    void FixedUpdate()
    {
        direction.x = -1f;
        //adjust y-position to be near player
        if (Mathf.Abs(rb.position.y - player.transform.position.y) > .5f)
        {
            direction.y = (player.transform.position.y - transform.position.y) / Mathf.Abs(player.transform.position.y - transform.position.y);
        }
        else direction.y = 0f;
        rb.MovePosition(rb.position + (direction * enemySpeed * Time.fixedDeltaTime));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
