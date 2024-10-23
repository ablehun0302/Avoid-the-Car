using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class BackendManager : MonoBehaviour
{
    void Start()
    {
        var bro = Backend.Initialize();

        if (bro.IsSuccess())
        {
            Debug.Log("초기화 성공 : " + bro);
        }
        else
        {
            Debug.LogError("초기화 실패 : " + bro);
        }

        Test();
    }

    void Test()
    {
        BackendLogin.Instance.GuestLogin();

        BackendGameLog.Instance.GameLogInsert();

        Debug.Log("테스트 종료");
    }
}