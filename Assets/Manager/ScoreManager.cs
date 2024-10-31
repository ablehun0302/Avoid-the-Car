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
    public int Score { get; set;} = 0;      //점수
    public int Second { get; set; } = 0;    //시간 표시용 변수
    public float Timer { get; set; } = 0f;

    public float SpeedFactor { get; set; } = 1f;
    
    public static ScoreManager Instance { get; set; }
    PlayerMovement player;
    AudioSource bgmusic;
    [SerializeField] ObstacleSpawner obstacleSpawner;

    void OnEnable()
    {
        Instance = this;
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
                //음악 피치조절
                bgmusic.pitch += 0.05f;
                
                //난이도 조절
                SpeedFactor += 0.11f;
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