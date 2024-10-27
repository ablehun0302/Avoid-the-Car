using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System;

public class BackendManager : MonoBehaviour
{
    string startTime;

    void Awake()
    {
#if UNITY_EDITOR
    Debug.unityLogger.logEnabled = true;
#else
    Debug.unityLogger.logEnabled = false;
#endif

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

        Login();
    }

    void Login()
    {
        BackendLogin.Instance.GuestLogin();

        BackendGameData.Instance.GameDataGet(); //데이터 불러오기

        // [추가] 서버에 불러온 데이터가 존재하지 않을 경우, 데이터를 새로 생성하여 삽입
        if (BackendGameData.userData == null)
        {
            BackendGameData.Instance.GameDataInsert();
        }
    }

    //게임 종료 시 로그 기록
    void OnApplicationQuit()
    {
        BackendGameData.Instance.GameDataUpdate();
        BackendGameLog.Instance.TimeLogInsert(startTime);
    }
}