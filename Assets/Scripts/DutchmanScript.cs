using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DutchmanScript : MonoBehaviour
{
    //assets for TFD and abilities
    public Image image;
    public GameObject monkeyChain1Prefab;
    public GameObject monkeyChain2Prefab;
    public GameObject monkeyFistPrefab;
    public GameObject monkeyPrefab;
    Color c;
    
    float elapsedTime = 0;
    float alphaTime = 3f;
    float killTime = 10f;
    bool chain = false;
    bool fist = false;
    bool monkey = false;

    // Start is called before the first frame update
    void Start()
    {
        c = image.color;
        c.a = 0;
        image.color = c;
        Destroy(gameObject, killTime);
    }

    void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;
        if (elapsedTime < alphaTime * (2/3f))//increase alpha
        {
            c = image.color;
            if(c.a <= .5f) c.a = (elapsedTime/2f);
            image.color = c;
        }
        else if (elapsedTime < alphaTime)//decrease alpha. Starts at 2 seconds in
        {
            c = image.color;
            if (c.a >= 0) c.a = (elapsedTime * -.5f) + 1.5f;
            image.color = c;
        }
        else if (!chain && elapsedTime > alphaTime + 1f)//THE MONKEY CHAIN
        {
            Instantiate(monkeyChain1Prefab);
            Instantiate(monkeyChain2Prefab);
            chain = true;
        }
        else if (!fist && elapsedTime > alphaTime + 1f + 3.5f)//THE MONKEY'S FIST
        {
            Instantiate(monkeyFistPrefab);
            fist = true;
        }
        else if (!monkey && elapsedTime > alphaTime + 1f + 3.5f + 2f)//THE MONKEY
        {
            Instantiate(monkeyPrefab);
            monkey = true;
        }
    }
}
