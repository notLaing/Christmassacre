using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;

    Vector2 direction;
    float enemySpeed = .5f;

    void FixedUpdate()
    {
        direction.x = -1f;
        direction.y = (player.transform.position.y - transform.position.y) / Mathf.Abs(player.transform.position.y - transform.position.y);
        rb.MovePosition(rb.position + (direction * enemySpeed * Time.fixedDeltaTime));
    }
}
