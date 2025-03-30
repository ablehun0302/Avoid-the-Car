using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePositioner : MonoBehaviour
{
    int outsideRadius = 40;
    float insideRadius = 15;

    PlayerMovement player;

    protected virtual void Start()
    {
        player = GameManager.Instance.GetPlayerMovement();
    }

    /// <summary>
    /// 맵 바깥쪽에 장애물을 랜덤으로 배치하는 메서드
    /// </summary>
    public void SetOutsidePosition()
    {
        float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));

        Vector2 randomPosition = (randomDirection * outsideRadius) + (Vector2)player.transform.position;

        transform.position = randomPosition;
    }

    /// <summary>
    /// 맵 안쪽에 장애물을 랜덤으로 배치하는 메서드
    /// </summary>
    /// <param name="obstacle"></param>
    public void SetInsidePosition()
    {
        float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        float randomRadius = Random.Range(0f, insideRadius);

        Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));

        Vector2 randomPosition = (randomDirection * randomRadius) + (Vector2)player.transform.position;

        transform.position = randomPosition;
    }

    /// <summary>
    /// 플레이어 방향으로 바라보는 메서드
    /// </summary>
    public void LookAtPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
