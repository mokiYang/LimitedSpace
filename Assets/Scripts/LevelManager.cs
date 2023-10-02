using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private CanvasGroup teach;

    private void Start()
    {
        GameObject teachObject = GameObject.FindWithTag("Teach");
        if (teachObject)
        {
            teach = GameObject.FindWithTag("Teach").GetComponent<CanvasGroup>();
        }
    }

    public void StartGame()
    {
        Debug.Log("start");
        if (teach)
        {
            teach.alpha = 0;
            teach.interactable = true;
            teach.blocksRaycasts = true;
        }
        else
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }

    public void Next()
    {
        Debug.Log("startGame");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void Retry()
    {
        Debug.Log("retry");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
