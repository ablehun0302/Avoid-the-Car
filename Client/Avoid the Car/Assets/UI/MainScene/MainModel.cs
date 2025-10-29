using UnityEngine;
using BackEnd;
using System;

public class MainModel : MonoBehaviour
{
    public string Nickname { get; private set; }
    public string[] FirstRankInfo { get; private set; }

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

    public string[] UpdateFirstRankInfo()
    {
        FirstRankInfo = BackendRank.Instance.FirstRankGet();
        Debug.Log(FirstRankInfo[0]+ "\n" + FirstRankInfo[1]);
        return FirstRankInfo;
    }
}
