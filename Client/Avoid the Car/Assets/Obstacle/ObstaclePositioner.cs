using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePositioner
{
    private static ObstaclePositioner instance = null;
    public static ObstaclePositioner Instance
    {
        get
        {
            if ( instance == null )
            {
                instance = new ObstaclePositioner();
            }
            return instance;
        }
    }

    PlayerMovement player = PlayerMovement.Instance;
    
    int outsideRadius = 30;
    float insideRadius = 10;

    /// <summary>
    /// 맵 바깥쪽에 장애물을 랜덤으로 배치하는 메서드
    /// </summary>
    public void SetOutsidePosition(GameObject obstacle)
    {
        float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        Vector2 randomDirection = new Vector2( Mathf.Cos(randomAngle), Mathf.Sin(randomAngle) );

        Vector2 randomPosition = (randomDirection * outsideRadius) + (Vector2)player.transform.position;

        obstacle.transform.position = randomPosition;
    }

    public void SetInsidePosition(GameObject obstacle)
    {
        float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        float randomRadius = Random.Range(0f, insideRadius);

        Vector2 randomDirection = new Vector2( Mathf.Cos(randomAngle), Mathf.Sin(randomAngle) );

        Vector2 randomPosition = (randomDirection * randomRadius) + (Vector2)player.transform.position;

        obstacle.transform.position = randomPosition;
    }

    /// <summary>
    /// 플레이어 방향으로 바라보는 메서드
    /// </summary>
    public void LookAtPlayer(GameObject obstacle)
    {
        Vector2 direction = (player.transform.position - obstacle.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        obstacle.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
