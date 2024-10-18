using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 점수를 화면에 띄우는 것을 구현.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set;} = 0;
    float timer = 0f;

    PlayerMovement player;

    void Start()
    {
        player = PlayerMovement.Instance;
    }

    void Update()
    {
        if (!player.enabled) { return; }

        timer += Time.deltaTime;
        if (timer >= 1)
        {
            Score ++;
            timer = 0;
        }
    }
}