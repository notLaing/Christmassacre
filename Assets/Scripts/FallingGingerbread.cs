using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGingerbread : MonoBehaviour
{
    float enemySpeed = 1f;
    float grav = -1f;
    float swingTime = 0f;
    float multiplier;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<EnemyHealthManager>().health = 4;
        GetComponent<EnemyHealthManager>().pointValue = 2;
    }

    void FixedUpdate()
    {
        swingTime += Time.fixedDeltaTime;
        multiplier = Mathf.Cos(swingTime * 2.5f) * 25f;//cos(theta) range: [-25, 25] degrees

        //move diagonally like a pendulum. X-direction slightly boosted to always move forward some amount with background
        transform.position += new Vector3((enemySpeed * multiplier * Time.fixedDeltaTime * .04f) - (enemySpeed * .85f * Time.fixedDeltaTime), grav * Time.fixedDeltaTime, 0f);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, multiplier));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
