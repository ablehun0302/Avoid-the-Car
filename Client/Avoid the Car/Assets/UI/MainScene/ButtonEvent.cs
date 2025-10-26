using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    MainPresenter presenter;

    [Header("메인씬 캔버스")]
    [SerializeField] GameObject titleCanvas;
    [SerializeField] GameObject mainGroup;
    [SerializeField] GameObject infoGroup;
    [SerializeField] GameObject inGameCanvas;
    [SerializeField] GameObject gameOverCanvas;

    [Header("텍스트")]
    [SerializeField] TMP_InputField nicknameInput;
    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] TextMeshProUGUI maxScoreText;
    [SerializeField] TextMeshProUGUI firstRankText;

    [Header("버튼")]
    [SerializeField] Button mainBtn;
    [SerializeField] Button updateNameBtn;
    [SerializeField] Button infoBtn;
    [SerializeField] Button infoToStartBtn;
    [SerializeField] Button startBtn;
    [SerializeField] Button restartBtn;
    [SerializeField] Button goToRankBtn;

    void Start()
    {
        // presenter에 view, model 연결
        presenter = new MainPresenter(this);

        // 버튼 상호작용 presenter로 전달
        mainBtn.onClick.AddListener(presenter.OnMainButtonClicked);
        updateNameBtn.onClick.AddListener(presenter.OnUpdateNameClicked);
        infoBtn.onClick.AddListener(presenter.OnInfoButtonClicked);
        infoToStartBtn.onClick.AddListener(presenter.OnStartButtonClicked);
        startBtn.onClick.AddListener(presenter.OnStartButtonClicked);
        restartBtn.onClick.AddListener(presenter.OnRestartButtonClicked);
        goToRankBtn.onClick.AddListener(presenter.OnGotoRankButtonClicked);
    }

    //텍스트 관련 메서드

    public string GetNicknameInput()
    {
        return nicknameInput.text;
    }

    public void SetNicknameInput(string name)
    {
        nicknameInput.text = name;
    }

    public void SetWarningText(string text)
    {
        warningText.text = text;
    }

    void SetMaxScoreText()
    {
        if (BackendGameData.userData == null) { return; }
        maxScoreText.text = "최고 점수: " + BackendGameData.userData.maxScore;
    }

    // 캔버스 관련 메서드

    public void MainUI(string rankText)
    {
        titleCanvas.SetActive(true);    // 켜기
        mainGroup.SetActive(true);      // 켜기
        infoGroup.SetActive(false);
        inGameCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);

        SetMaxScoreText();
        warningText.text = "";
        firstRankText.text = rankText;
    }

    public void InfoUI()
    {
        infoGroup.SetActive(true);  // 켜기
        mainGroup.SetActive(false);
    }

    public void InGameUI()
    {
        inGameCanvas.SetActive(true);   // 켜기
        titleCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    // 씬 이동 관련 메서드

    public void GoToRanking()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}