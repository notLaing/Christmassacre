using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyScript : MonoBehaviour
{
    float speed = 5f;
    float jitterSize = .15f;
    float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, -10f, 0f);
        Destroy(gameObject, 6f);
    }

    void FixedUpdate()
    {
        if (transform.position.y < -1.5f && elapsedTime == 0f)
        {
            transform.position += new Vector3(0f, 1f, 0f) * Time.fixedDeltaTime * speed;
            Jitter();
        }
        else elapsedTime += Time.fixedDeltaTime;

        if(elapsedTime > 3f)
        {
            transform.position -= new Vector3(0f, 1f, 0f) * Time.fixedDeltaTime * speed;
            Jitter();
        }
    }

    void Jitter()
    {
        transform.position += new Vector3(jitterSize, 0f, 0f);
        jitterSize *= -1;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Debug.Log("THE MONKEY!!!");
            collider.gameObject.GetComponent<EnemyHealthManager>().health -= 5;
        }
    }
}
