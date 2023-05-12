using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;

    public float transitionTime = 1f;


    

    public void LoadGameScene()
    {
        StartCoroutine(LoadGameLevel());
    }

    IEnumerator LoadGameLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("atu");
    }

    public void ExitGame()
    {
        StartCoroutine(ExitLevel());
    }

    IEnumerator ExitLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        Application.Quit();
    }
}
