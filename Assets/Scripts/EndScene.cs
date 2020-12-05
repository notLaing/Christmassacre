using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public void Restart()
    {
        if(SceneManager.GetActiveScene().buildIndex == 5)//victory screen
        {
            GameManagerScript.resetGame();
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().RestartLevel();
        }
        else
        {
            GameManagerScript.score = GameManagerScript.stageScore;
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().RestartLevel();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
