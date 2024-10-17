using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 점수를 화면에 띄우는 것을 구현.
/// </summary>
public class Scoreboard : MonoBehaviour
{
    int score = 0;
    float timer = 0f;

    PlayerMovement player;
    TMP_Text scoreText;

    void Start()
    {
        player = PlayerMovement.Instance;
        scoreText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (!player.enabled) { return; }

        timer += Time.deltaTime;
        if (timer >= 1)
        {
            score ++;
            timer = 0;
        }
        scoreText.text = "점수: " + score.ToString();
    }
}
