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
    public int Score { get; set; } = 0;
    public int ElapsedSec { get; set; } = 0;
    public int SecDividedByTen { get; set; } = 0;

    public float SpeedFactor { get; set; } = 1f;

    //난이도, 점수 조절값
    int eventInterval = 20;
    int scoreIncreasement = 10;
    float pitchIncreasement = 0.05f;

    float speedIncreasement = 0.11f;
    float spawnRateReduction = 0.1f;
    float specialRateReduction = 0.2f;

    public static ScoreManager Instance { get; set; }
    PlayerMovement player;
    GameManager gameManager;
    AudioSource bgmusic;
    [SerializeField] ObstacleSpawner obstacleSpawner;
    [SerializeField] GameObject fireworkVFX;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = PlayerMovement.Instance;
        gameManager = GameManager.Instance;
        bgmusic = player.GetComponent<AudioSource>();
    }

    /// <summary>
    /// 시간 경과에 의해 실행되는 행동들
    /// </summary>
    /// <returns></returns>
    IEnumerator TimeBasedEvents()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            Score += scoreIncreasement;
            SecDividedByTen ++;
            if (SecDividedByTen >= 10)
            {
                ElapsedSec++;
                SecDividedByTen = 0;
            }
            
            if (SecDividedByTen != 0) { continue; }

            //Debug.Log("ElapsedSec : " + ElapsedSec);
            //20초마다 실행되는
            if (ElapsedSec % eventInterval == 0)
            {
                bgmusic.pitch += pitchIncreasement;

                SpeedFactor += speedIncreasement;
                obstacleSpawner.CurrentSpawnRate -= spawnRateReduction;
                if (ElapsedSec != 20) { obstacleSpawner.CurrentSpecialRate -= specialRateReduction; }

                /*Debug.Log("-------------\nSpeedFactor: " + SpeedFactor
                         +"\nSpawnRate: " + obstacleSpawner.CurrentSpawnRate
                         +"\nSpecialRate: " + obstacleSpawner.CurrentSpecialRate);*/
            }

            //특정 초마다 추가되는
            switch (ElapsedSec)
            {
                case 20:
                    obstacleSpawner.SpawnSpecial();
                    break;
            }
        }
    }

    public void EventCoroutine()
    {
        StartCoroutine(TimeBasedEvents());
    }

    public IEnumerator IncreaseBonusScore()
    {
        yield return null;
        if (!gameManager.IsGameOver)
        {
            Score += 1000;
            Instantiate(fireworkVFX, player.transform.position, fireworkVFX.transform.rotation);
            //Debug.Log("보너스점수");
        }
    }
}