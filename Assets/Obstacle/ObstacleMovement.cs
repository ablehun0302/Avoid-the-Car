using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장애물의 움직임을 구현
/// </summary>
public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] string obstacleName;
    ScoreManager scoreManager;
    GameManager gameManager;

    Rigidbody2D rigidBody;
    Transform front;

    void OnEnable()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        front = transform.GetChild(0);
        gameObject.name = obstacleName;
        scoreManager = ScoreManager.Instance;
        gameManager = GameManager.Instance;
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
            StartCoroutine(IncreaseBonusScore());
        }
    }

    IEnumerator IncreaseBonusScore()
    {
        yield return null;
        if (!gameManager.IsGameOver)
        {
            scoreManager.Score += 1000;
            Debug.Log("보너스점수");
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