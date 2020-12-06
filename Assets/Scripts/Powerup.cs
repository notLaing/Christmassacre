using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    float obstacleSpeed = 3f;
    float bobTime = 0f;
    float multiplier;

    void FixedUpdate()
    {
        //move in a straight line, bobbing up and down
        bobTime += Time.fixedDeltaTime;
        multiplier = Mathf.Cos(bobTime * 4f) * .75f;
        transform.position += new Vector3((-obstacleSpeed * Time.fixedDeltaTime), (multiplier * Time.fixedDeltaTime), 0f);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
