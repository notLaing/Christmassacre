using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPoints : MonoBehaviour
{
    public Text curScore;
    public Text bestScore;
    // Start is called before the first frame update
    void Start()
    {
        curScore.text = GameManagerScript.score.ToString();
        bestScore.text = GameManagerScript.highScore.ToString();
    }
}
