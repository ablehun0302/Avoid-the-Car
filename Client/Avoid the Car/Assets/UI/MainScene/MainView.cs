using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainView : MonoBehaviour
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
    [SerializeField] TextMeshProUGUI inGameFirstRankText;
    [SerializeField] TextMeshProUGUI inGameScoreText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI lastScoreText;
    [SerializeField] TextMeshProUGUI lastTimerText;

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

    public void SetScoreText(int score)
    {
        string scoreText = "점수: " + score;
        inGameScoreText.text = scoreText;
        lastScoreText.text = scoreText;
    }

    public void SetTimerText(int secDividedByTen, int elapsedSec)
    {
        int millisec = secDividedByTen * 10;
        int sec = elapsedSec % 60;
        int minute = elapsedSec / 60;

        string millisecString = millisec.ToString("D2");
        string secString = sec.ToString("D2");

        string timeText = $"{minute}:{secString}.{millisecString}";

        timerText.text = "시간 "+timeText;
        lastTimerText.text = "버틴시간 "+timeText;
    }

    void SetMaxScoreText()
    {
        if (BackendGameData.userData == null) { return; }
        maxScoreText.text = "최고 점수: " + BackendGameData.userData.maxScore;
    }

    void SetMainFirstRankText(string[] rankInfo)
    {
        if (rankInfo != null)
        {
            firstRankText.text = string.Format("{0}이(가) 1등 기록중!", rankInfo[0]);
        }
        else
        {
            firstRankText.text = "랭킹 정보를 불러올 수 없습니다.";
        }
    }

    void SetInGameFirstRankText(string[] rankInfo)
    {
        if (rankInfo != null)
        {
            inGameFirstRankText.text = string.Format("{0}의 최고기록\n{1}점을 넘겨보자!", rankInfo[0], rankInfo[1]);
        }
        else
        {
            inGameFirstRankText.text = "최대한 오래 버텨보자!";
        }
    }

    // 캔버스 관련 메서드

    public void MainUI(string[] rankInfo)
    {
        titleCanvas.SetActive(true);    // 켜기
        mainGroup.SetActive(true);      // 켜기
        infoGroup.SetActive(false);
        inGameCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);

        SetMaxScoreText();
        SetMainFirstRankText(rankInfo);
        warningText.text = "";
    }

    public void InfoUI()
    {
        infoGroup.SetActive(true);  // 켜기
        mainGroup.SetActive(false);
    }

    public void InGameUI(string[] rankInfo)
    {
        inGameCanvas.SetActive(true);   // 켜기
        titleCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        SetInGameFirstRankText(rankInfo);
    }

    public void GameOverUI()
    {
        inGameCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    }

    // 씬 이동 관련 메서드

    public void GoToRanking()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}