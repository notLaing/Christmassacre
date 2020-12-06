using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    GameObject[] transitionObjects = new GameObject[2];
    public float transitionTime = .1f;

    void Start()
    {
        //get the gameobjects so we can set them active as we need them
        transitionObjects[0] = transform.Find("Crossfade_GameObject").gameObject;
        transitionObjects[1] = transform.Find("PanDown_GameObject").gameObject;
        transitionObjects[0].SetActive(true);

        //specific case for transitioning INTO Level 1
        //if (SceneManager.GetActiveScene().buildIndex == 1 && !GameManagerScript.gameOverScreen && !(GameManagerScript.restarting)) transitionObjects[1].SetActive(true);
        GameManagerScript.restarting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.P))
        {
            LoadGameOver();
        }
        else if (Input.GetKeyDown("space") && SceneManager.GetActiveScene().buildIndex == 0)
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        //specific case for transitioning OUT OF title screen
        /*if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            transitionObjects[0].SetActive(false);//set fade to false, since title screen only needs to and has already faded in
            transitionObjects[1].SetActive(true);//set pan to true, since now we need to pan
            transitionObjects[1].GetComponentInChildren<Animator>().SetTrigger("Start");
        }*/
        transitionObjects[0].SetActive(true);
        //doesn't matter if this triggers on the title screen since its gameObject isn't active anymore
        transitionObjects[0].GetComponentInChildren<Animator>().SetTrigger("Start");

        //load next level based on build settings
        GameManagerScript.gameOverScreen = false;
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)//levelIndex is the next level
    {
        //if not game over, update the stage count. For game over navigation purposes
        if (levelIndex != 6) GameManagerScript.stage = levelIndex;

        //Play animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(levelIndex);
    }


    public void LoadGameOver()
    {
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        //reset any necessary variables
        if(SceneManager.GetActiveScene().buildIndex == GameManagerScript.dutchmanStage)
        {
            GameManagerScript.dutchmanStage = 0;
            GameManagerScript.dutchmanReady = true;
        }


        //Play animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene("GameOver");
    }



    public void RestartLevel()
    {
        StartCoroutine(RestartLevelRoutine());
    }

    IEnumerator RestartLevelRoutine()
    {
        GameManagerScript.restarting = true;
        //reset any necessary variables
        if (SceneManager.GetActiveScene().buildIndex == GameManagerScript.dutchmanStage)
        {
            GameManagerScript.dutchmanStage = 0;
            GameManagerScript.dutchmanReady = true;
        }


        //set up any necessary elements specifically for level 1
        if (GameManagerScript.stage == 1)//(SceneManager.GetActiveScene().buildIndex == 1)
        {
            transitionObjects[0].SetActive(true);
            transitionObjects[1].SetActive(false);
            transitionObjects[0].GetComponentInChildren<Animator>().SetTrigger("Start");
        }


        //Play animation
        else transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(GameManagerScript.stage);
    }
}
