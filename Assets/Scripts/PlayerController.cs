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
    GameObject[] enemiesOnScreen;
    public float moveSpeed = 5f;
    public float levelCountdown = 3f;
    public Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;
    Color spriteColor;
    float invincibilityTimer = 0f;
    public int health = 5;
    public int secondaryResource = 0;
    public int stageKills = 0;
    public bool powerUp = false;
    bool progressing = false;

    //bullet variables
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    float angle = 0f;
    float shootTimer = 0f;

    //power up variables
    public GameObject dutchman;
    public GameObject screenWipe;

    //UI variables
    public GameObject resourceUI;
    GameObject healthUI;
    GameObject secondaryResourceUI;
    public GameObject transitioner;
    public Text[] pointText;

    //Jack Frost
    public GameObject JackFrost;
    bool spawnedJack = false;
    public bool beatJack = false;

    void Start()
    {
        cam = Camera.main;
        transitioner = GameObject.Find("LevelLoader");
        pointText = GameObject.Find("PointUI").GetComponentsInChildren<Text>();
        spriteColor = GetComponent<SpriteRenderer>().color;
        resourceUI = GameObject.Find("UI");
        healthUI = resourceUI.transform.Find("Canvas").gameObject.transform.Find("Empty Bars").gameObject.transform.Find("Health").gameObject;
        secondaryResourceUI = resourceUI.transform.Find("Canvas").gameObject.transform.Find("Empty Bars").gameObject.transform.Find("Resource").gameObject;
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
            if (GameManagerScript.score > 99)
            {
                if(GameManagerScript.dutchmanReady)
                {
                    Instantiate(dutchman);
                    GameManagerScript.dutchmanReady = false;
                    GameManagerScript.dutchmanStage = SceneManager.GetActiveScene().buildIndex;
                }
                else
                {
                    //text for dutchman already being used
                }
                
            }
            else
            {
                //make text that shows that dutchman hasn't been unlocked yet
            }
        }


        if (Input.GetKeyDown("space"))
        {
            if (secondaryResource > 0)
            {
                Instantiate(screenWipe);
                --secondaryResource;
                updateResource();
            }
        }

        //manually reduce health
        if (Input.GetKeyDown(KeyCode.Y))
        {
            health -= 1;
            if (health < 0) health = 0;
            updateResource();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            health += 1;
            if (health > 5) health = 5;
            updateResource();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ++secondaryResource;
            if (secondaryResource > 4) secondaryResource = 4;
            updateResource();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameManagerScript.score += 50;
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
                pointText[0].text = GameManagerScript.score.ToString();
                pointText[1].text = GameManagerScript.highScore.ToString();
                break;
        }
    }

    void FixedUpdate()
    {
        //movement bounds
        if ((movement.x == 1 && !(rb.position.x < -1)) || (movement.x == -1 && !(rb.position.x > -8))) movement.x = 0;
        if ((movement.y == 1 && !(rb.position.y < 4)) || (movement.y == -1 && !(rb.position.y > -2.8))) movement.y = 0;

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

        //progress levels
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                if (stageKills > 19 && !progressing)
                {
                    levelPrep();
                    progressing = true;
                }
                break;
            case 2:
                if (stageKills > 24 && !progressing)
                {
                    levelPrep();
                    progressing = true;
                }
                break;
            case 3:
                if (stageKills > 29 && !progressing)
                {
                    levelPrep();
                    progressing = true;
                }
                break;
            case 4:
                if (stageKills > 34 && !progressing)
                {
                    levelPrep();
                    if (!spawnedJack)
                    {
                        Instantiate(JackFrost);
                        spawnedJack = true;
                    }
                    if (beatJack) progressing = true;
                }
                break;
        }
        if(progressing)//might not be getting called when you first pass the condition
        {
            levelCountdown -= Time.fixedDeltaTime;
            enemiesOnScreen = GameObject.FindGameObjectsWithTag("Enemy");
            if (levelCountdown < 0f && enemiesOnScreen.Length == 0) GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel();
        }
    }



    void OnTriggerEnter2D(Collider2D collider)
    {
        if (invincibilityTimer <= 0)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                --health;
                invincibilityTimer = 1.5f;
            }
            else if (collider.gameObject.tag == "Enemy Bullet")
            {
                --health;
                invincibilityTimer = 1.5f;
            }
        }

        if (collider.gameObject.tag == "Powerup")
        {
            ++secondaryResource;
        }
        updateResource();
    }

    void Shoot()
    {
        //create bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
    }


    void levelPrep()
    {
        GameObject spawner = GameObject.Find("Spawner (Right)");
        spawner.GetComponent<EnemySpawner_Right>().spawn = false;
    }


    void updateResource()
    {
        //update health bar
        switch(health)
        {
            case 5:
                healthUI.transform.Find("100").gameObject.SetActive(true);
                break;
            case 4:
                healthUI.transform.Find("100").gameObject.SetActive(false);
                break;
            case 3:
                healthUI.transform.Find("80").gameObject.SetActive(false);
                break;
            case 2:
                healthUI.transform.Find("60").gameObject.SetActive(false);
                break;
            case 1:
                healthUI.transform.Find("40").gameObject.SetActive(false);
                break;
            case 0:
                healthUI.transform.Find("20").gameObject.SetActive(false);
                break;
        }

        //update secondary resource bar
        switch (secondaryResource)
        {
            case 0:
                secondaryResourceUI.transform.Find("25").gameObject.SetActive(false);
                break;
            case 1:
                secondaryResourceUI.transform.Find("25").gameObject.SetActive(true);
                secondaryResourceUI.transform.Find("50").gameObject.SetActive(false);
                break;
            case 2:
                secondaryResourceUI.transform.Find("50").gameObject.SetActive(true);
                secondaryResourceUI.transform.Find("75").gameObject.SetActive(false);
                break;
            case 3:
                secondaryResourceUI.transform.Find("75").gameObject.SetActive(true);
                secondaryResourceUI.transform.Find("100").gameObject.SetActive(false);
                break;
            case 4:
                secondaryResourceUI.transform.Find("100R").gameObject.SetActive(true);
                break;
        }
    }
}
