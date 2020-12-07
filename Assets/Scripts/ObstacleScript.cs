using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    Animator anim;
    float obstacleSpeed = 3f;
    float bobTime = 0f;
    float multiplier;

    void Start()
    {
        //don't play the explosion animation on start
        anim = transform.Find("Obstacle").GetComponent<Animator>();
        anim.speed = 0f;
    }

    void FixedUpdate()
    {
        //move in a straight line, bobbing up and down
        bobTime += Time.fixedDeltaTime;
        multiplier = Mathf.Cos(bobTime * 4f) * .75f;
        transform.position += new Vector3((-obstacleSpeed * Time.fixedDeltaTime), (multiplier * Time.fixedDeltaTime), 0f);

        if (transform.position.x < -11f) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (collider.gameObject.tag == "Player")
        {
            anim.speed = 1f;
            Destroy(gameObject, 1f);
        }
    }
}
