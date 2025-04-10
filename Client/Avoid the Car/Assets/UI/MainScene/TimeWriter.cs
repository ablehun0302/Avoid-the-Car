using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeWriter : MonoBehaviour
{
    int milisec = 0;
    int sec = 0;
    int minute = 0;

    string milisecString;
    string secString;

    ScoreManager scoreManager;
    TextMeshProUGUI timerText;
    [SerializeField] string sourceText;

    void Start()
    {
        scoreManager = GameManager.Instance.GetScoreManager();
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        milisec = scoreManager.SecDividedByTen * 10;
        sec = scoreManager.ElapsedSec % 60;
        minute = scoreManager.ElapsedSec / 60;

        milisecString = milisec.ToString("D2");
        secString = sec.ToString("D2");

        timerText.text = sourceText + " " +minute + ":" + secString + "." + milisecString;
    }
}
