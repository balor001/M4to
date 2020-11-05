using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_PanelManager : MonoBehaviour
{
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
        Debug.Log("sceneName to load: " + scenename);
        SceneManager.LoadScene(scenename);
    }
}
