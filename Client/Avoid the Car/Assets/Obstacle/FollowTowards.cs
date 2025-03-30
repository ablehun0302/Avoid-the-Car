using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTowards : MonoBehaviour
{
    [SerializeField] float turnSpeed = 10f; // 최대 회전 각도
    [SerializeField] float followTimes = 10f;
    float timer = 0f;

    PlayerMovement player;
    ScoreManager scoreManager;
    Rigidbody2D thisRigidbody;

    void Start()
    {
        GameManager gameManager = GameManager.Instance;
        
        player = gameManager.GetPlayerMovement();
        scoreManager = gameManager.GetScoreManager();
        thisRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (timer > followTimes) { return; }
        RotateTowards();
        timer += Time.deltaTime;
    }

    /// <summary>
    /// 특정 방향으로 회전하는 메서드
    /// </summary>
    void RotateTowards()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // 목표 각도 (디그리 값)
        float currentAngle = thisRigidbody.rotation; // 현재 각도 (디그리 값)
        float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle) * Mathf.Deg2Rad; // 각도 차이 계산

        // 각도 차이에 비례하는 회전력 추가
        float torque = angleDifference * turnSpeed * scoreManager.SpeedFactor * thisRigidbody.inertia;
        thisRigidbody.AddTorque(torque, ForceMode2D.Force);
    }
}