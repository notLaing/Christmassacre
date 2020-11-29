using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject controlsMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused) Resume();
            else Pause();
        }
    }



    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void Controls()
    {
        controlsMenu.SetActive(true);
    }

    public void OutOfControls()
    {
        controlsMenu.SetActive(false);
    }

    public void Restart()
    {
        GameManagerScript.score = GameManagerScript.stageScore;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        gamePaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
