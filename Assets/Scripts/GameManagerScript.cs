using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameManagerScript
{
    /*#region Singleton

    public static GameManagerScript instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
    */


    //global variables
    public static int score = 0;
    public static int stageScore = 0;
    public static int highScore = 0;
    public static int stage = 0;
    public static bool gameOverScreen = false;

    public static void reset()
    {
        if (highScore < score) highScore = score;
        score = 0;
        //send back to scene 1 (level 1)
    }
}
