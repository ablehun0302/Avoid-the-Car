using UnityEngine;
using BackEnd;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BackendManager : MonoBehaviour
{
    string startTime;
    [SerializeField] GameObject idField;
    [SerializeField] GameObject pwField;
    [SerializeField] TextMeshProUGUI infoText;

    void Awake()
    {
#if UNITY_EDITOR
    Debug.unityLogger.logEnabled = true;
#else
    Debug.unityLogger.logEnabled = false;
#endif
        //DontDestroyOnLoad(gameObject);

        //게임 시작 시 들어온 시간 체크
        startTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        var bro = Backend.Initialize();

        if (bro.IsSuccess())
        {
            Debug.Log("초기화 성공 : " + bro);
        }
        else
        {
            Debug.LogError("초기화 실패 : " + bro);
        }

        Login(); //게스트로그인
    }

    void Login()
    {
        BackendLogin.Instance.GuestLogin();

        BackendGameData.Instance.GameDataGet(); //데이터 불러오기

        // [추가] 서버에 불러온 데이터가 존재하지 않을 경우, 데이터를 새로 생성하여 삽입
        if (BackendGameData.userData == null)
        {
            BackendGameData.Instance.GameDataInsert();
            BackendLogin.Instance.CreateNickname();
        }

        BackendRank.Instance.RankInsert(BackendGameData.userData.maxScore);
    }

    public void CustomLoginButton()
    {
        TMP_InputField idInput = idField.GetComponent<TMP_InputField>();
        TMP_InputField pwInput = pwField.GetComponent<TMP_InputField>();

        string idText = idInput.text;
        string pwText = pwInput.text;

        int LoginState = BackendLogin.Instance.CustomLogin(idText, pwText);

        Debug.Log(LoginState);

        if (LoginState != 0)
        {
            infoText.text = "아이디와 비밀번호가 일치하지 않거나, 형식에 맞지 않습니다."+
                            "\n모든 문자는 영문 또는 숫자여야 합니다.";
            return;
        }

        BackendGameData.Instance.GameDataGet(); //데이터 불러오기

        // [추가] 서버에 불러온 데이터가 존재하지 않을 경우, 데이터를 새로 생성하여 삽입
        if (BackendGameData.userData == null)
        {
            BackendGameData.Instance.GameDataInsert();

            BackendLogin.Instance.CreateNickname();
        }

        BackendRank.Instance.RankInsert(BackendGameData.userData.maxScore);

        SceneManager.LoadScene(1);
    }

    //게임 종료 시 로그 기록
    void OnApplicationQuit()
    {
        if (BackendGameData.Instance == null) { return; }
        BackendGameData.Instance.GameDataUpdate();
        BackendGameLog.Instance.TimeLogInsert(startTime);
    }
}