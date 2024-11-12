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
    [SerializeField] int[] spawnFrequency;
    [SerializeField] float spawnRate = 1;
    int spawnFrequencyLength;
    public float CurrentSpawnRate { get; set; }
    
    [Header("특수 장애물")]
    [SerializeField] GameObject[] specialPrefabs;
    [SerializeField] int[] specialCounts;
    [SerializeField] float specialRate = 10;
    public float CurrentSpecialRate { get; set; }

    //PlayerMovement player;
    [SerializeField] Transform obstaclePool;

    void Awake()
    {
        foreach (int frequency in spawnFrequency)
        {
            spawnFrequencyLength += frequency;
        }
    }

    void OnEnable()
    {
        //player = PlayerMovement.Instance;
        
        //기본값 초기화
        CurrentSpawnRate = spawnRate;
        CurrentSpecialRate = specialRate;

        //장애물 생성
        StopAllCoroutines();
        StartCoroutine(SpawnObstaclesRoutine());
    }

    /// <summary>
    /// 장애물을 생성하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnObstaclesRoutine()
    {
        while (true)
        {
            //장애물 종류 정하기
            int randomValue = Random.Range(0, spawnFrequencyLength);
            int frequencyMinValue = 0;
            for (int index = 0; index < obstaclePrefabs.Length; index ++)
            {
                if ( randomValue >= frequencyMinValue && randomValue < spawnFrequency[index] + frequencyMinValue )
                {
                    Instantiate(obstaclePrefabs[index], obstaclePool);
                    break;
                }

                frequencyMinValue += spawnFrequency[index];
            }

            CurrentSpawnRate = Mathf.Clamp(CurrentSpawnRate, 0.5f, 1f);
            yield return new WaitForSeconds(CurrentSpawnRate);
        }
    }

    /// <summary>
    /// 특수 장애물을 생성하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnSpecialRoutine()
    {
        while(true)
        {
            //장애물 종류 정하기
            int index = Random.Range(0, specialPrefabs.Length);
            int count = specialCounts[index];
            for (int i = 0; i < count; i++)
            {
                Instantiate(specialPrefabs[index], obstaclePool);
            }

            yield return new WaitForSeconds(CurrentSpecialRate);
        }
    }

    /// <summary>
    /// 특수 장애물 생성 코루틴 실행 메서드
    /// </summary>
    public void SpawnSpecial()
    {
        StartCoroutine(SpawnSpecialRoutine());
    }
}