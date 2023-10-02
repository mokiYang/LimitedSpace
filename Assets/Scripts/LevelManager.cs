using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private CanvasGroup teach;

    private void Start()
    {
        teach = GameObject.FindWithTag("Teach").GetComponent<CanvasGroup>();
    }

    public void start()
    {
        teach.alpha = 0;
        teach.interactable = true;
        teach.blocksRaycasts = true;
    }

    public void next()
    {
        Debug.Log("startGame");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void retry()
    {
        Debug.Log("retry");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
