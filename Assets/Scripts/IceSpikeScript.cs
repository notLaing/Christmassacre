using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeScript : MonoBehaviour
{
    public GameObject hit;
    float elapsedTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        hit = transform.Find("Hitboxes").gameObject;
        transform.position = new Vector3(Random.Range(-7.4f, -1.5f), -1.2f, 0f);
    }


    void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;
        //once the beginning part of the animation is over, move the hitbox
        if (elapsedTime >= .67f)
        {
            hit.transform.localPosition = new Vector3(0f, 0f, 0f);
            if (elapsedTime > 2f) Destroy(gameObject);
        }
    }
}
