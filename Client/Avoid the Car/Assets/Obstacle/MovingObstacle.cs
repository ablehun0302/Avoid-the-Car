using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장애물의 움직임을 구현
/// </summary>
public class MovingObstacle : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] string obstacleName;

    Rigidbody2D rigidBody;
    Transform front;
    ScoreManager scoreManager;
    ObstaclePositioner obstaclePositioner;

    void Start()
    {
        gameObject.name = obstacleName;
        rigidBody = GetComponent<Rigidbody2D>();
        front = transform.GetChild(0);
        scoreManager = ScoreManager.Instance;
        obstaclePositioner = ObstaclePositioner.Instance;

        obstaclePositioner.SetOutsidePosition(gameObject);
        obstaclePositioner.LookAtPlayer(gameObject);
    }

    void FixedUpdate()
    {
        MoveForward();
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Area")) { Destroy(gameObject); }
        if (other.CompareTag("Player"))
        {
            StartCoroutine(scoreManager.IncreaseBonusScore());
        }
    }

    /// <summary>
    /// 오브젝트가 정면으로 이동하는 메서드
    /// </summary>
    void MoveForward()
    {
        Vector2 direction = (front.position - transform.position).normalized;

        rigidBody.AddForce(direction * speed * scoreManager.SpeedFactor, ForceMode2D.Force);
    }
}