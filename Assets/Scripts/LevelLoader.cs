using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    GameObject player;
    Scene currentScene;
    string m_Scene;
    public float transitionTime = .1f;

    // Update is called once per frame
    void Update()
    {
        //DontDestroyOnLoad(gameObject);
        if (Input.GetKeyDown("space"))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Pressed G");
            LoadGameOver();
            Debug.Log("Check");
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)//levelIndex is the next level
    {
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
        //Play animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene("GameOver");
    }
}
