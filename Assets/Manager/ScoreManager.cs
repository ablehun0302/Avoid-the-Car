using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 점수와 관련된 행동을 구현
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public int Score { get; set;} = 0;
    public float Timer { get; set; } = 0f;

    PlayerMovement player;
    AudioSource bgmusic;
    [SerializeField] ObstacleSpawner obstacleSpawner;

    void Start()
    {
        player = PlayerMovement.Instance;
        bgmusic = player.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!player.enabled) { return; }

        Timer += Time.deltaTime;
        if (Timer >= 1)
        {
            Score ++;
            Timer = 0;

            if (Score % 20 == 0 && Score > 0)
            {
                bgmusic.pitch += 0.05f;

                obstacleSpawner.CurrentSpawnRate -= 0.1f;
                if (Score != 20) { obstacleSpawner.CurrentSpecialRate -= 0.2f; }
            }

            switch (Score)
            {
                case 20:
                    obstacleSpawner.SpawnSpecial();
                    break;
            }
        }
    }
}