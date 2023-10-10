using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class UI_UIManager : MonoBehaviour
{

   public Scene currentScene;
   public Scene nextScene;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        Debug.Log("current scene:" + currentScene);
        nextScene = SceneManager.GetSceneByBuildIndex(currentScene.buildIndex + 1);
        Debug.Log("next scene:" + nextScene);

    }

    public void Switch()
    {
        if (gameObject.activeSelf) gameObject.SetActive(false);
        else if (!gameObject.activeSelf) gameObject.SetActive(true);
    }

    public void Enable()
    {
        //If the gameobject is active, disables it and vice versa
        if (!gameObject.activeSelf) gameObject.SetActive(true);
    }
    public void Disable()
    {
        if (gameObject.activeSelf) gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
        //Just to make sure its working
    }

    public void NextScene()
    {
        currentScene = SceneManager.GetActiveScene();
        int nextBuildIndex = currentScene.buildIndex + 1;

        if (nextBuildIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Loading the next scene in index: " + nextBuildIndex, this);
            SceneManager.LoadScene(nextBuildIndex);
        }
        else
        {
            Debug.LogError("No Valid Scene to Load", this);
        }
    }

    public void LoadScene(string scenename)
    {

        if (scenename == "" && nextScene.IsValid())
        {
            Debug.LogWarning("Loading the next scene in index!", this);
            SceneManager.LoadScene(nextScene.name);
        }
        else if (!nextScene.IsValid() && scenename == "")
        {
            Debug.LogError("Scene does not exist! Please set a correct scene or disable this button.", this);
        }

        else
        {
            Debug.Log("sceneName to load: " + scenename);
            SceneManager.LoadScene(scenename);
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }
}
