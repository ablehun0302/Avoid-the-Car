using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장애물을 생성하는 것을 구현
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] float spawnRate = 1;

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

            //장애물 종류 정하기

            int index = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[index]);
        }
    }

    void CancelSpawnObstacles()
    {
        StopCoroutine(SpawnObstacles());
    }
}