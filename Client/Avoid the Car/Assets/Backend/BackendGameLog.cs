using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System;

public class BackendGameLog
{
    static BackendGameLog _instance = null;

    public static BackendGameLog Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendGameLog();
            }

            return _instance;
        }
    }

    /// <summary>
    /// 들어온/ 나간 시간을 로그에 입력하는 메서드
    /// </summary>
    /// <param name="startTime">들어온 시간</param>
    public void TimeLogInsert(string startTime)
    {
        string exitTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        Param param = new Param();
        
        param.Add("exitTime", exitTime);
        param.Add("startTime", startTime);

        Debug.Log("게임 로그 삽입을 시도합니다.");

        var bro = Backend.GameLog.InsertLogV2("ExitGame", param);

        if (bro.IsSuccess() == false)
        {   
            Debug.LogError("게임 로그 삽입 중 에러가 발생했습니다. : " + bro);
            return;
        }

        Debug.Log("게임 로그 삽입에 성공했습니다. : " + bro);
    }

    /// <summary>
    /// 유저가 죽었을 때 몇 점대 인지, 무엇에 부딪혔는지를 로그에 입력하는 메서드
    /// </summary>
    /// <param name="score">점수</param>
    /// <param name="something">부딛힌 장애물</param>
    public void DeadLogInsert(int score, string something)
    {
        int scoreRange = ( score / 10 ) * 10;

        Param param = new Param();
        
        param.Add("scoreRange", scoreRange);
        param.Add("deadBy", something);

        Debug.Log("게임 로그 삽입을 시도합니다.");

        var bro = Backend.GameLog.InsertLogV2("GameScore", param);

        if (bro.IsSuccess() == false)
        {   
            Debug.LogError("게임 로그 삽입 중 에러가 발생했습니다. : " + bro);
            return;
        }

        Debug.Log("게임 로그 삽입에 성공했습니다. : " + bro);
    }
}
