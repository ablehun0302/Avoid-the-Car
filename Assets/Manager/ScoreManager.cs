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
    public int Score { get; set;} = 0;
    public float Timer { get; set; } = 0f;

    PlayerMovement player;
    [SerializeField] ObstacleSpawner obstacleSpawner;

    void Start()
    {
        player = PlayerMovement.Instance;
    }

    void Update()
    {
        if (!player.enabled) { return; }

        Timer += Time.deltaTime;
        if (Timer >= 1)
        {
            Score ++;
            Timer = 0;

            switch (Score)
            {
                case 20:
                    obstacleSpawner.SpawnSpecial();
                    break;
            }
        }
    }
}