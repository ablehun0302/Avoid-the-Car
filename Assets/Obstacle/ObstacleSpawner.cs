using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장애물을 생성하는 것을 구현
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] float spawnRate = 1;

    [SerializeField] int minRandomRange = -6;
    [SerializeField] int maxRandomRange = 13;

    [SerializeField] int minSpawnRange = 3;
    [SerializeField] int maxSpawnRange = 95;

    PlayerMovement player;

    void Start()
    {
        player = PlayerMovement.Instance; 
        StartCoroutine(SpawnObstacles());
    }

    /// <summary>
    /// 장애물을 생성하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            //플레이어 기준 일정 범위 내에서 장애물 생성
            float playerYPos = player.transform.position.y;
            float spawnPosY = playerYPos + Random.Range(minRandomRange, maxRandomRange);

            Vector2 spawnPos = new Vector2(-23, spawnPosY);
            if (spawnPos.y < minSpawnRange || spawnPos.y > maxSpawnRange) { continue; }

            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }
}
