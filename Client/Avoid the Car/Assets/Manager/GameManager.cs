using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using DG.Tweening;
using TMPro;

/// <summary>
/// 게임의 흐름을 제어하는 메서드 모음
/// </summary>
public class GameManager : MonoBehaviour
{
    public bool IsGameOver { get; private set; } = false;
    
    Vector3 textScale = new(1, 1, 1);

    PlayerMovement player;
    ScoreManager scoreManager;
    PlayerCollision playerCollision;
    Rigidbody2D playerRigidbody;
    PlayerInput playerInput;
    public AudioSource bgmusic;
    [SerializeField] CinemachineVirtualCamera followCamera;
    [SerializeField] GameObject obstacleSpawner;
    [SerializeField] Transform obstaclePool;
    [Header("타이틀 캔버스 관련 필드")]
    [SerializeField] GameObject titleCanvas;
    [Header("인게임 캔버스 관련 필드")]
    [SerializeField] GameObject inGameCanvas;
    [SerializeField] GameObject[] startTexts;
    [SerializeField] GameObject firstRankText;
    [Header("게임오버 캔버스 관련 필드")]
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] TextMeshProUGUI subText;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = PlayerMovement.Instance;
        scoreManager = ScoreManager.Instance;
        playerCollision = player.GetComponent<PlayerCollision>();
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerInput = player.GetComponent<PlayerInput>();
        bgmusic = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!IsGameOver) { return; }
        if (bgmusic.pitch > 0) { bgmusic.pitch -= Time.deltaTime / 2; }
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

        //scoreManager 초기화
        scoreManager.Score = 0;
        scoreManager.ElapsedSec = 0;
        scoreManager.SecDividedByTen = 0;
        scoreManager.SpeedFactor = 1f;

        BackendGameLog.Instance.DashCount = 0;
        BackendGameLog.Instance.DashSuccessNumber = 0;
        BackendGameLog.Instance.ItemUseCount = 0;
        BackendGameLog.Instance.ItemSuccessNumber = 0;

        IsGameOver = false;

        bgmusic.Stop();
        bgmusic.pitch = 1;
        bgmusic.Play();
    }

    /// <summary>
    /// 게임 시작 메서드
    /// </summary>
    public void GameStart()
    {
        titleCanvas.SetActive(false);
        inGameCanvas.SetActive(true);   //인게임 캔버스 켜기
        gameOverCanvas.SetActive(false);

        Sequence countSequence = DOTween.Sequence();
        for (int i = 0; i < startTexts.Length; i++)
        {
            int index = i; // Capture the current value of i
            countSequence.Append(startTexts[index].transform.DOScale(textScale, 1).SetEase(Ease.OutBack).OnComplete(() =>
            {
                if (index == startTexts.Length - 1)
                {
                    GameSet();
                }
                else
                {
                    startTexts[index].transform.localScale = Vector3.zero;
                }
            }));
        }
        countSequence.Insert(0f, firstRankText.transform.DOScale(textScale, 1).SetEase(Ease.OutSine));
        countSequence.Append(startTexts[startTexts.Length - 1].transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutSine));
        countSequence.Join(firstRankText.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutSine));
    }

    /// <summary>
    /// 게임 시작 시 세팅 메서드
    /// </summary>
    void GameSet()
    {
        //플레이어 움직임 활성화
        player.enabled = true;
        playerInput.enabled = true;

        followCamera.enabled = true;

        obstacleSpawner.SetActive(true);

        //스코어 더하기 실행
        scoreManager.EventCoroutine();
    }

    /// <summary>
    /// 게임오버 시 실행되는 메서드
    /// </summary>
    public void GameOver()
    {
        IsGameOver = true;

        //장애물에 부딛힐 시 플레이어 움직임 정지
        player.enabled = false;
        playerInput.enabled = false;
        playerRigidbody.drag = 2; //사망 모션

        followCamera.enabled = false;

        obstacleSpawner.SetActive(false);

        //스코어 더하기 중지
        scoreManager.StopAllCoroutines();

        inGameCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        
        //랭킹 비교 후 텍스트 변경
        if (BackendGameData.userData == null) return;
        List<string> firstRankInfo = BackendRank.Instance.FirstRankGet();
        string firstRankNickname = firstRankInfo[0];
        int firstRankScore = int.Parse(firstRankInfo[1]);

        if (scoreManager.Score > firstRankScore)
        {
            mainText.text = "Congratulations!";
            subText.text = "1등은 이제 당신입니다!!";
        }
        else
        {
            mainText.text = "Game Over!";
            subText.text = firstRankNickname + "보다는 한참 모자라시네요! ㅠㅜ";
        }
    }
}