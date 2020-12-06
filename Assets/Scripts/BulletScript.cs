using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //public GameObject hitEffect;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            collider.gameObject.GetComponent<EnemyHealthManager>().health--;
        }

        if (collider.gameObject.tag != "Powerup") Destroy(gameObject);
    }
}
