using UnityEngine;
using BackEnd;
using System;
using System.Collections.Generic;

public class MainModel : MonoBehaviour
{
    public string Nickname { get; private set; }
    string firstRankTextFormat = "{0}이(가) 1등 기록중!";

    public void GetUserNickname(Action<string> onComplete)
    {
        Debug.Log("유저 닉네임 불러오기 시도");
        Backend.BMember.GetUserInfo(callback =>
        {
            if (callback.IsSuccess())
            {
                Nickname = callback.GetReturnValuetoJSON()["row"]["nickname"].ToString();
                onComplete?.Invoke(Nickname);
                Debug.Log("불러오기 성공");
            }
            else
            {
                onComplete?.Invoke(string.Empty);
                Debug.LogWarning("불러오기 실패");
            }
        });
    }

    public void InitializeNickname()
    {
        BackendLogin.Instance.CreateNickname();
        Backend.BMember.GetUserInfo(callback =>
        {
            Nickname = callback.GetReturnValuetoJSON()["row"]["nickname"].ToString();
        });
    }

    /// <summary>
    /// 닉네임을 변경하는 메서드
    /// </summary>
    /// <param name="nickname"></param>
    /// <returns>변경 상태 번호</returns>
    public int UpdateNickname(string nickname)
    {
        return BackendLogin.Instance.UpdateNickname(nickname);
    }

    public string GetFirstRankText()
    {
        List<string> firstRankInfo = BackendRank.Instance.FirstRankGet();
        string result;

        if (firstRankInfo != null && firstRankInfo.Count >= 2)
        {
            string playerName = firstRankInfo[0]; // 첫 번째 요소
            string playerScore = firstRankInfo[1]; // 두 번째 요소

            result = string.Format(firstRankTextFormat, playerName, playerScore);
        }
        else
        {
            result = "랭킹 정보를 불러올 수 없습니다.";
        }

        return result;
    }
}
