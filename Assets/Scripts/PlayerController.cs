using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //player variables
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;

    //bullet variables
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    float angle = 0f;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        //movement bounds
        if ((movement.x == 1 && !(rb.position.x < -1)) || (movement.x == -1 && !(rb.position.x > -8))) movement.x = 0;
        if ((movement.y == 1 && !(rb.position.y < 4)) || (movement.y == -1 && !(rb.position.y > -4))) movement.y = 0;

        rb.MovePosition(rb.position + (movement * moveSpeed * Time.fixedDeltaTime));

        //rotate the "gun"
        Vector2 lookDir = mousePos - rb.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        if (angle > 60f) angle = 60f;
        else if (angle < -60f) angle = -60f;
        firePoint.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }



    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Debug.Log("Player touched enemy");
        }
    }

    void Shoot()
    {
        //create bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
    }
}
