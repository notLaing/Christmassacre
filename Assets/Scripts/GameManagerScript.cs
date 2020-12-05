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
    public static int dutchmanStage = 0;
    public static bool dutchmanReady = true;
    public static bool gameOverScreen = false;
    public static bool restarting = false;

    public static void resetGame()//resets the game from the victory screen
    {
        if (highScore < score) highScore = score;
        score = 0;
        stageScore = 0;
        stage = 1;
        dutchmanReady = true;
        dutchmanStage = 0;
        gameOverScreen = false;
        //send back to scene 1 (level 1)
    }
}
