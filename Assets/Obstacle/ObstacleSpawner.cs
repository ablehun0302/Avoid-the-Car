using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장애물을 생성하는 것을 구현
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    [Header("기본 장애물")]
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] float spawnRate = 1;
    
    [Header("특수 장애물")]
    [SerializeField] GameObject[] specialPrefabs;
    [SerializeField] float specialRate = 10;

    //[Header("스폰 설정")]
    int radius = 30;
    bool isSpawnSpecial = false;

    PlayerMovement player;
    [SerializeField] ScoreManager scoreManager;

    void Start()
    {
        player = PlayerMovement.Instance;
        StartCoroutine(SpawnObstacles());
    }

    void Update()
    {
        if (!isSpawnSpecial && scoreManager.Score == 20)
        { 
            StartCoroutine(SpawnSpecial());
            isSpawnSpecial = true;
        }
    }

    /// <summary>
    /// 장애물을 생성하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            //장애물 종류 정하기
            int index = Random.Range(0, obstaclePrefabs.Length);
            GameObject obstacle = Instantiate(obstaclePrefabs[index]);
            SetFirstPosition(obstacle);
            LookAtPlayer(obstacle);

            yield return new WaitForSeconds(spawnRate);
        }
    }

    /// <summary>
    /// 특수 장애물을 생성하는 코루틴
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpawnSpecial()
    {
        while(true)
        {
            //장애물 종류 정하기
            int index = Random.Range(0, specialPrefabs.Length);
            GameObject obstacle = Instantiate(specialPrefabs[index]);
            SetFirstPosition(obstacle);
            LookAtPlayer(obstacle);

            yield return new WaitForSeconds(specialRate);
        }
    }

    /// <summary>
    /// 맵 바깥쪽에 장애물을 랜덤으로 배치하는 메서드
    /// </summary>
    void SetFirstPosition(GameObject obstacle)
    {
        float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;

        Vector2 randomDirection = new Vector2( Mathf.Cos(randomAngle), Mathf.Sin(randomAngle) );

        Vector2 randomPosition = randomDirection * radius;

        obstacle.transform.position = randomPosition;
    }

    /// <summary>
    /// 플레이어 방향으로 바라보는 메서드
    /// </summary>
    void LookAtPlayer(GameObject obstacle)
    {
        Vector2 direction = (player.transform.position - obstacle.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        obstacle.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}