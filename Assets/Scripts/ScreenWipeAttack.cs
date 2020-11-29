using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenWipeAttack : MonoBehaviour
{
    public Image circle;
    public Color c;
    float elapsedTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        c = circle.color;
        c.a = 0;
        circle.color = c;
        Destroy(gameObject, 1f);
    }

    void FixedUpdate()
    {
        c.a += .05f;
        circle.color = c;
    }
}
