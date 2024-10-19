using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장애물의 움직임을 구현
/// </summary>
public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] float speed = 10;

    Rigidbody2D rigidBody;
    PlayerMovement player;
    Transform front;

    void OnEnable()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = PlayerMovement.Instance;
        front = transform.GetChild(0);
    }

    void FixedUpdate()
    {
        MoveForward();
        Despawn();
    }

    /// <summary>
    /// 오브젝트가 정면으로 이동하는 메서드
    /// </summary>
    void MoveForward()
    {
        Vector2 direction = (front.position - transform.position).normalized;

        //rigidBody.velocity = direction * speed;
        rigidBody.AddForce(direction * speed, ForceMode2D.Force);
    }

    /// <summary>
    /// 장애물이 맵을 벗어나면 사라지도록 하는 메서드
    /// </summary>
    void Despawn()
    {
        if (Vector2.Distance( Vector2.zero, transform.position) > 40) { Destroy(gameObject); }
    }
}