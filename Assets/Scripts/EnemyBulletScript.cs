using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    //public GameObject hitEffect;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            //collider.gameObject.GetComponent<PlayerController>().health -= 10;
            Destroy(gameObject);
        }
        else if (collider.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
