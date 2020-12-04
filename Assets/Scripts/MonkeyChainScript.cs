using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyChainScript : MonoBehaviour
{
    float angle = Mathf.Atan2(9, 16) * Mathf.Rad2Deg;
    float speed = 25f;
    float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-24f, -13.5f, 0f);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        angle *= Mathf.Deg2Rad;
        Destroy(gameObject, 3f);
    }

    void FixedUpdate()
    {
        if (transform.position.x < 0f && elapsedTime == 0f) transform.position += new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * Time.fixedDeltaTime * speed;
        else elapsedTime += Time.fixedDeltaTime;

        if (elapsedTime > .85f) transform.position -= new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * Time.fixedDeltaTime * speed * 4f;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Debug.Log("THE MONKEY CHAIN!");
            collider.gameObject.GetComponent<EnemyHealthManager>().health -= 5;
        }
    }
}
