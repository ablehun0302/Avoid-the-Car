using System.Collections;
using System.Collections.Generic;

public class MainPresenter
{
    GameManager gameManager;
    MainModel mainModel;
    ButtonEvent mainView;

    public MainPresenter(ButtonEvent view)
    {
        gameManager = GameManager.Instance;
        mainModel = gameManager.GetMainModel();
        mainView = view;

        Initialize();
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

        mainView.MainUI(mainModel.GetFirstRankText());
    }

    public void OnMainButtonClicked()
    {
        gameManager.GameReset();
        mainView.MainUI(mainModel.GetFirstRankText());
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
        mainView.InGameUI();
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
