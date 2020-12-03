using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_UIManager : MonoBehaviour
{

    Scene currentScene;
    Scene nextScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        nextScene = SceneManager.GetSceneByBuildIndex(currentScene.buildIndex + 1);
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
