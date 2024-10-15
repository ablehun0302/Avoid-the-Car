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
    [SerializeField] int minYRange = -6;
    [SerializeField] int maxYRange = 13;

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

            //플레이어 기준 y -10 ~ 10 범위에서 장애물 생성
            float playerYPos = player.transform.position.y;
            float spawnPosY = playerYPos + Random.Range(minYRange, maxYRange);

            Vector2 spawnPos = new Vector2(-18, spawnPosY);
            if (spawnPos.y <= 2 || spawnPos.y >= 95) { continue; }

            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }
}
