using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //player variables
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;
    Color spriteColor;
    float invincibilityTimer = 0f;
    public int health = 100;
    public bool powerUp = false;

    //bullet variables
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    float angle = 0f;
    float shootTimer = 0f;

    //power up variables
    public GameObject dutchman;

    //UI variables
    public GameObject transitioner;
    public Text pointText;

    void Start()
    {
        cam = Camera.main;
        transitioner = GameObject.Find("LevelLoader");
        pointText = GameObject.Find("PointUI").GetComponentInChildren<Text>();
        spriteColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        invincibilityTimer -= Time.deltaTime;
        if (invincibilityTimer < 0f) invincibilityTimer = 0f;
        shootTimer += Time.deltaTime;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //automatic fire
        if (Input.GetButton("Fire1"))
        {
            if (shootTimer > .3f)
            {
                shootTimer = 0f;
                Shoot();
            }
        }

        //dutchman power up
        if (Input.GetButtonDown("Fire2"))
        {
            var createImage = Instantiate(dutchman) as GameObject;
        }

        //manually reduce health
        if (Input.GetKeyDown(KeyCode.Q))
        {
            health -= 10;
            Debug.Log(health);
        }

        //point text only on levels 1-4
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0://title
                break;
            case 5://victory
                break;
            case 6://game over
                break;
            default:
                pointText.text = "Points: " + GameManagerScript.score;
                break;
        }
    }

    void FixedUpdate()
    {
        //movement bounds
        if ((movement.x == 1 && !(rb.position.x < -1)) || (movement.x == -1 && !(rb.position.x > -8))) movement.x = 0;
        if ((movement.y == 1 && !(rb.position.y < 4)) || (movement.y == -1 && !(rb.position.y > -4))) movement.y = 0;

        rb.MovePosition(rb.position + (movement * moveSpeed * Time.fixedDeltaTime));

        //rotate the cannon aim
        Vector2 lookDir = mousePos - rb.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        if (angle > 60f) angle = 60f;
        else if (angle < -60f) angle = -60f;
        firePoint.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        //flash the sprite after taking damage
        if (Mathf.Floor(invincibilityTimer * 10f) % 2 == 0)
        {
            spriteColor.a = 1f;
            GetComponent<SpriteRenderer>().color = spriteColor;
        }
        else
        {
            spriteColor.a = 0f;
            GetComponent<SpriteRenderer>().color = spriteColor;
        }

        //player death loads game over
        if (health <= 0) transitioner.GetComponent<LevelLoader>().LoadGameOver();
    }



    void OnTriggerEnter2D(Collider2D collider)
    {
        if (invincibilityTimer <= 0)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                health -= collider.gameObject.GetComponent<EnemyHealthManager>().damageValue;
                Debug.Log("Remaining health: " + health);
            }
            else if (collider.gameObject.tag == "Enemy Bullet")
            {
                health -= 10;
                Debug.Log("Remaining health: " + health);
            }

            invincibilityTimer = 1.5f;
        }
    }

    void Shoot()
    {
        //create bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
    }
    /*void flash()
    {
        if (Mathf.Floor(invincibilityTimer * 10f) % 2 == 0) spriteColor.a = 1f;
        else spriteColor.a = 0f;
    }*/
}
