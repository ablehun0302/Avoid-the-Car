using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

/// <summary>
/// 플레이어 충돌과 이후 이벤트를 구현
/// </summary>
public class PlayerCollision : MonoBehaviour
{
    public bool IsGameOver { get; set; } = false;
    AudioSource bgmusic;
    Animator animator;
    [SerializeField] ScoreManager scoreManager;

    void Start()
    {
        bgmusic = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!IsGameOver) { return; }
        if (bgmusic.pitch > 0) { bgmusic.pitch -= Time.deltaTime / 2; }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Obstacle") || IsGameOver) { return; }
        
        IsGameOver = true;

        //게임오버 동작 실행
        animator.SetBool("isDead", true);
        GameManager.Instance.GameOver();

        //유저 점수 로그 입력
        BackendGameLog.Instance.DeadLogInsert(scoreManager.Score, other.gameObject.name);

        //유저 데이터 수정
        BackendGameData.Instance.UserDataSet(scoreManager.Score, 0, other.gameObject.name);
        BackendGameData.Instance.GameDataUpdate();
    }
}