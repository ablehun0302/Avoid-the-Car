using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장애물의 움직임을 구현
/// </summary>
public class MovingObstacle : ObstaclePositioner
{
    [SerializeField] float speed = 10;
    [SerializeField] int bonusScore;
    [SerializeField] string obstacleName;

    Rigidbody2D rigidBody;
    Transform front;
    ScoreManager scoreManager;

    protected override void Start()
    {
        base.Start();
        gameObject.name = obstacleName;
        rigidBody = GetComponent<Rigidbody2D>();
        front = transform.GetChild(0);
        scoreManager = GameManager.Instance.GetScoreManager();

        SetOutsidePosition();
        LookAtPlayer();
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
            StartCoroutine(scoreManager.IncreaseBonusScore(bonusScore));
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