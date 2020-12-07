using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;

public class Game_Controller : MonoBehaviour, IUnityAdsListener
{
    public enum LevelPlayState { InProgress, Won, Lost, Reset, Quit }
    private LevelPlayState state = LevelPlayState.InProgress;

    Scene thisScene;

    public Canvas winUI;
    public Canvas loseUI;

    // Amount of points needed to win
    public int winCondition = 3;
    public bool play = true;

    private float secondsElapsed = 0;
    private int gameOvers = 0;
    private int wins = 0;

    private void Awake()
    {
        Advertisement.Initialize("3903621", false); // inizialize advertisements with Android GameID

        // Start level analytics
        thisScene = SceneManager.GetActiveScene();
        AnalyticsEvent.LevelStart(thisScene.name, thisScene.buildIndex);
        StartGameSetup();
        play = true;
    }

    public void SetLevelPlayState(LevelPlayState newState)
    {
        this.state = newState;
    }

    private void Update()
    {
        secondsElapsed += Time.deltaTime;
    }

    void StartGameSetup()
    {
        play = true;
        winUI.gameObject.SetActive(false);
        loseUI.gameObject.SetActive(false);
        SetLevelPlayState(LevelPlayState.InProgress);
    }

    public void WinLevel()
    {
        play = false;
        wins++;
        winUI.gameObject.SetActive(true);
        ShowInterstitialAd();
        SetLevelPlayState(LevelPlayState.Won);
    }

    public void GameOver()
    {
        gameOvers++;
        loseUI.gameObject.SetActive(true);
        ShowInterstitialAd();
        SetLevelPlayState(LevelPlayState.Lost);
    }

    void OnDestroy()
    {
        Dictionary<string, object> customParams = new Dictionary<string, object>();
        customParams.Add("seconds_played", secondsElapsed);
        customParams.Add("wins", wins);
        customParams.Add("game_overs", gameOvers);

        switch (this.state)
        {
            case LevelPlayState.Won:
                AnalyticsEvent.LevelComplete(thisScene.name, thisScene.buildIndex, customParams);
                break;
            case LevelPlayState.Lost:
                AnalyticsEvent.LevelFail(thisScene.name, thisScene.buildIndex, customParams);
                break;
            case LevelPlayState.Reset:
                AnalyticsEvent.LevelSkip(thisScene.name, thisScene.buildIndex, customParams);
                break;
            case LevelPlayState.InProgress:
            case LevelPlayState.Quit:
            default:
                AnalyticsEvent.LevelQuit(thisScene.name, thisScene.buildIndex, customParams);
                break;
        }
    }

    #region Advertisement
    public void ShowInterstitialAd()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public interface IUnityAdsListener
    {
        void OnUnityAdsReady(string placementId);
        void OnUnityAdsDidError(string message);
        void OnUnityAdsDidStart(string placementId);
        void OnUnityAdsDidFinish(string placementId, ShowResult showResult);
    }

    public void OnUnityAdsReady(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    void UnityEngine.Advertisements.IUnityAdsListener.OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
