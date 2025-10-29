using System.Collections;
using System.Collections.Generic;

public class MainPresenter
{
    GameManager gameManager;
    ScoreManager scoreManager;
    MainModel mainModel;

    MainView mainView;

    public MainPresenter(MainView view)
    {
        gameManager = GameManager.Instance;
        scoreManager = gameManager.GetScoreManager();
        mainModel = gameManager.GetMainModel();
        mainView = view;

        Initialize();
        //model -> presenter 수신
        scoreManager.OnScoreChanged += () =>
            mainView.SetScoreText(scoreManager.Score);
        scoreManager.OnScoreChanged += () =>
            mainView.SetTimerText(scoreManager.SecDividedByTen, scoreManager.ElapsedSec);
        gameManager.OnGameOver += mainView.GameOverUI;
    }

    void Initialize()
    {
        mainModel.GetUserNickname(nickname =>
        {
            if (string.IsNullOrEmpty(nickname))
            {
                mainModel.InitializeNickname();
            }
            mainView.SetNicknameInput(mainModel.Nickname);
        });

        mainView.MainUI(mainModel.UpdateFirstRankInfo());
    }

    //텍스트 관련 메서드


    //버튼 관련 메서드
    public void OnMainButtonClicked()
    {
        gameManager.GameReset();
        mainView.MainUI(mainModel.UpdateFirstRankInfo());
    }

    public void OnUpdateNameClicked()
    {
        switch (mainModel.UpdateNickname(mainView.GetNicknameInput()))
        {
            case 204:
                mainView.SetWarningText("*닉네임 변경에 성공하였습니다!");
                break;
            case 409:
                mainView.SetWarningText("*중복된 닉네임 입니다. 다른 닉네임으로 설정해주세요.");
                break;
            default:
                mainView.SetWarningText("*닉네임 변경에 실패하였습니다. 형식에 맞게 설정해주세요.");
                break;
        }
    }

    public void OnInfoButtonClicked()
    {
        mainView.InfoUI();
    }

    public void OnStartButtonClicked()
    {
        mainView.InGameUI(mainModel.FirstRankInfo);
        gameManager.GameStart();
    }

    public void OnRestartButtonClicked()
    {
        gameManager.GameReset();
        OnStartButtonClicked();
    }

    public void OnGotoRankButtonClicked()
    {
        mainView.GoToRanking();
    }
}
