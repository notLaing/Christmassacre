using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyFistScript : MonoBehaviour
{
    float speed = 35f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-15f, 0f, 0f);
        Destroy(gameObject, 1.5f);
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(1f, 0f, 0f) * Time.fixedDeltaTime * speed;
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Debug.Log("THE MONKEY'S FIST!");
            collider.gameObject.GetComponent<EnemyHealthManager>().health -= 5;
        }
    }
}
