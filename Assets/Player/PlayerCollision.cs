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
    bool isGameOver = false;

    [SerializeField] CinemachineVirtualCamera followCamera;
    [SerializeField] ObstacleSpawner obstacleSpawner;
    [SerializeField] GameObject inGameCanvas;
    [SerializeField] GameObject gameOverCanvas;
    PlayerMovement playerMovement;
    AudioSource bgmusic;

    void Start()
    {
        playerMovement = PlayerMovement.Instance;
        bgmusic = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isGameOver) { return; }
        if (bgmusic.pitch > 0) { bgmusic.pitch -= Time.deltaTime / 2; }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Obstacle")) { return; }
        
        isGameOver = true;
        
        //장애물에 부딛힐 시 플레이어 움직임 정지
        playerMovement.enabled = false;

        //플레이어 공기저항 2로 변경 -> 사망 모션
        playerMovement.GetComponent<Rigidbody2D>().drag = 2;

        //카메라 비활성화
        followCamera.enabled = false;

        //장애물 생성 중지
        obstacleSpawner.StopAllCoroutines();

        //재시작 버튼 표시, 점수판 캔버스 비활성화
        gameOverCanvas.SetActive(true);
        inGameCanvas.SetActive(false);
    }
}
