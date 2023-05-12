using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EscPraSair : MonoBehaviour
{

    public Animator transition;

    public float transitionTime = 1f;


    IEnumerator EscPraSairr()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("TelaInicial");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(EscPraSairr());
        }
    }
}
