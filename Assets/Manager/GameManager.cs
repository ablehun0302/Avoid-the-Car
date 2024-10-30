using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

/// <summary>
/// 게임의 흐름을 제어하는 메서드 모음
/// </summary>
public class GameManager : MonoBehaviour
{
    PlayerMovement player;
    PlayerCollision playerCollision;
    Rigidbody2D playerRigidbody;
    PlayerInput playerInput;
    AudioSource bgmusic;
    ScoreManager scoreManager;
    [SerializeField] CinemachineVirtualCamera followCamera;
    [SerializeField] GameObject obstacleSpawner;
    [SerializeField] Transform obstaclePool;
    [SerializeField] GameObject titleCanvas;
    [SerializeField] GameObject inGameCanvas;
    [SerializeField] GameObject gameOverCanvas;

    public static GameManager Instance { get; private set; }

    void Start()
    {
        Instance = this;

        player = PlayerMovement.Instance;
        playerCollision = player.GetComponent<PlayerCollision>();
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerInput = player.GetComponent<PlayerInput>();
        bgmusic = player.GetComponent<AudioSource>();
        scoreManager = GetComponent<ScoreManager>();
    }

    /// <summary>
    /// 게임 초기화 메서드
    /// </summary>
    public void GameReset()
    {
        //기존 장애물 제거
        foreach (Transform obstacle in obstaclePool)
        {
            Destroy(obstacle.gameObject);
        }

        //점수 0으로 초기화
        scoreManager.Score = 0;
        scoreManager.Timer = 0;

        playerCollision.IsGameOver = false;
        bgmusic.Stop();
        bgmusic.pitch = 1;
        bgmusic.Play();
    }

    /// <summary>
    /// 게임 시작/재시작 시 실행되는 메서드
    /// </summary>
    public void GameStart()
    {
        //플레이어 움직임 활성화
        player.enabled = true;
        playerInput.enabled = true;

        //카메라 활성화
        followCamera.enabled = true;

        //장애물 생성
        obstacleSpawner.SetActive(true);

        titleCanvas.SetActive(false);
        inGameCanvas.SetActive(true);   //인게임 캔버스 켜기
        gameOverCanvas.SetActive(false);
    }

    /// <summary>
    /// 게임오버 시 실행되는 메서드
    /// </summary>
    public void GameOver()
    {
        //장애물에 부딛힐 시 플레이어 움직임 정지
        player.enabled = false;
        playerInput.enabled = false;
        //플레이어 공기저항 2로 변경 -> 사망 모션
        playerRigidbody.drag = 2;

        //카메라 비활성화
        followCamera.enabled = false;

        //장애물 생성 중지
        obstacleSpawner.SetActive(false);

        //재시작 버튼 표시, 점수판 캔버스 비활성화
        inGameCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    }
}