using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float length = 0, startX;
    public float parallaxEffect = 0;

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
    }

    void FixedUpdate()
    {
        //move the background layer
        transform.position = new Vector3(transform.position.x - (parallaxEffect * Time.fixedDeltaTime), transform.position.y, transform.position.z);

        //reset position
        if(transform.position.x < startX - length)
        {
            transform.position = new Vector3(transform.position.x + length, transform.position.y, transform.position.z);
        }
    }
}
