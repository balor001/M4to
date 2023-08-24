using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{
    Player_Controller player_Controller;

    // Start is called before the first frame update
    void Start()
    {
        player_Controller = FindObjectOfType<Player_Controller>();
    }

    public void ScoreUI(int score, int totalScore)
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = score + " / " + totalScore;
    }
}
