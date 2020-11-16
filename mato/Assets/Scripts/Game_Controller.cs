using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour
{

    public float restartDelay = 1f;

    public Canvas winUI;
    public Canvas loseUI;
    
    // Amount of points needed to win
    public float winCondition = 3;

    private void Start()
    {
        StartGameSetup();
    }

    void StartGameSetup()
    {
        winUI.gameObject.SetActive(false);
        loseUI.gameObject.SetActive(false);
    }

    public void WinLevel()
    {
        Debug.Log("You Win!");
        winUI.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        loseUI.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartGameSetup();
    }
}
