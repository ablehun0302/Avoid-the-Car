using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장애물의 움직임을 구현
/// </summary>
public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] float speed = 10;

    int radius = 30;

    Rigidbody2D rigidBody;
    PlayerMovement player;
    Transform front;

    void OnEnable()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = PlayerMovement.Instance;
        front = transform.GetChild(0);

        SetFirstPosition();
        LookAtPlayer();
    }

    void FixedUpdate()
    {
        MoveForward();
        Despawn();
    }

    /// <summary>
    /// 맵 바깥쪽에 장애물을 랜덤으로 배치하는 메서드
    /// </summary>
    void SetFirstPosition()
    {
        float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;

        Vector2 randomDirection = new Vector2( Mathf.Cos(randomAngle), Mathf.Sin(randomAngle) );

        Vector2 randomPosition = randomDirection * radius;

        transform.position = randomPosition;
    }

    /// <summary>
    /// 플레이어 방향으로 바라보는 메서드
    /// </summary>
    void LookAtPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    /// <summary>
    /// 오브젝트가 정면으로 이동하는 메서드
    /// </summary>
    void MoveForward()
    {
        Vector2 direction = (front.position - transform.position).normalized;

        rigidBody.velocity = direction * speed;
    }

    /// <summary>
    /// 장애물이 맵을 벗어나면 사라지도록 하는 메서드
    /// </summary>
    void Despawn()
    {
        if (Vector2.Distance( Vector2.zero, transform.position) > 35) { Destroy(gameObject); }
    }
}