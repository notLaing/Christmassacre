using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenWipeAttack : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject player;
    public Image circle;
    public Color c;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Hero(Clone)");
        c = circle.color;
        c.a = 0;
        circle.color = c;
        circle.rectTransform.position = new Vector3(100f, 410f, 0f);// + new Vector3(8.9f * player.transform.position.x, 5f * player.transform.position.x, 0);
        //Destroy(gameObject, 1f);
    }

    void FixedUpdate()
    {
        //increase alpha (white "flash") and size
        c.a += .05f;
        circle.color = c;
        circle.rectTransform.localScale += new Vector3(.2f, .2f, 0);

        //at the end of the attack, actually apply damage to anything on screen
        if(circle.rectTransform.localScale.x > 5f)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; ++i)
            {
                enemies[i].GetComponent<EnemyHealthManager>().health -= 10;
            }
            Destroy(gameObject);
        }
    }
}
