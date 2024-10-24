using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System;

public class BackendManager : MonoBehaviour
{
    string startTime;

    void Start()
    {
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
    }

    //게임 종료 시 로그 기록
    void OnApplicationQuit()
    {
        BackendGameLog.Instance.TimeLogInsert(startTime);
    }
}