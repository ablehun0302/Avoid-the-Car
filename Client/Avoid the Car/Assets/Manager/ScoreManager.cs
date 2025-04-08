using System.Collections;
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
    public Coroutine coroutine;

    //난이도, 점수 조절값
    int spawnEventInterval = 10;
    int speedEventInterval = 20;
    int miscSpawnInterval = 30;
    int scoreIncreasement = 10;
    float pitchIncreasement = 0.05f;

    float speedIncreasement = 0.1f;
    int spawnRateReduction = 1;
    int specialRateReduction = 2;

    GameManager gameManager;
    PlayerMovement player;
    DeathCollision deathCollision;
    //TriggerCollision triggerCollision;
    AudioSource bgmusic;
    AudioSource bonusScoreSound;
    [SerializeField] ObstacleSpawner obstacleSpawner;
    [SerializeField] GameObject fireworkVFX;
    [SerializeField] GameObject bonusScoreText;

    void Start()
    {
        gameManager = GameManager.Instance;
        player = gameManager.GetPlayerMovement();
        deathCollision = player.GetComponentInChildren<DeathCollision>();
        bgmusic = gameManager.bgmusic;
        bonusScoreSound = player.GetComponent<AudioSource>();
    }

    /// <summary>
    /// 시간 경과에 의해 실행되는 행동들
    /// </summary>
    /// <returns></returns>
    public IEnumerator TimeBasedEvents()
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

            //10초마다 실행되는 (스폰 주기 설정)
            if (ElapsedSec % spawnEventInterval == 0)
            {
                if (obstacleSpawner.CurrentSpawnRate > 5) { obstacleSpawner.CurrentSpawnRate -= spawnRateReduction; }
                if (ElapsedSec != spawnEventInterval) { obstacleSpawner.CurrentSpecialRate -= specialRateReduction; }
            }

            //20초마다 실행되는 (속도 주기 설정)
            if (ElapsedSec % speedEventInterval == 0)
            {
                bgmusic.pitch += pitchIncreasement;
                SpeedFactor += speedIncreasement;
            }

            if (ElapsedSec % miscSpawnInterval == 0)
            {
                obstacleSpawner.SpawnItem();
            }

            //특정 초마다 추가되는
            switch (ElapsedSec)
            {
                case 10:
                    obstacleSpawner.SpawnSpecial();
                    break;
            }
        }
    }

    public IEnumerator IncreaseBonusScore(int bonusScore)
    {
        yield return null;
        if (!gameManager.IsGameOver)
        {
            Score += bonusScore;
            if (bonusScore >= 1000)
            {
                for (int i = 0; i < 2; i ++) {Instantiate(fireworkVFX, player.transform.position, fireworkVFX.transform.rotation);}
            }
            Instantiate(fireworkVFX, player.transform.position, fireworkVFX.transform.rotation);
            GameObject bonusText = Instantiate(bonusScoreText, player.transform.position, bonusScoreText.transform.rotation);
            bonusText.GetComponent<TextMeshPro>().text = "+ " + bonusScore +"점";

            bonusScoreSound.Play();

            if (deathCollision.IsInvulnerable) 
            {
                BackendGameLog.Instance.ItemSuccessNumber ++;
            }
            else 
            {
                BackendGameLog.Instance.DashSuccessNumber ++;
            }
        }
    }

    public void ResetScoreStats()
    {
        Score = 0;
        ElapsedSec = 0;
        SecDividedByTen = 0;
        SpeedFactor = 1;
    }
}