using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{
    public void ManageScoreUI(int score, int totalScore)
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = score + " / " + totalScore;
    }
}
