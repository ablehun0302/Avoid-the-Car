using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.SceneManagement;

public class BackendLogin
{
    static BackendLogin _instance = null;

    public static BackendLogin Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendLogin();
            }

            return _instance;
        }
    }

    /// <summary>
    /// 게스트 회원가입/로그인 메서드
    /// </summary>
    public void GuestLogin()
    {
        Debug.Log("게스트 로그인을 요청합니다.");

        var bro = Backend.BMember.GuestLogin();

        if (bro.IsSuccess())
        {
            Debug.Log("로그인이 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("로그인이 실패했습니다. : " + bro);
            if (bro.StatusCode == 401)
            {
                Backend.BMember.DeleteGuestInfo();
                Debug.LogWarning("기기 내 게스트 정보 삭제");
            }
        }
    }

    public int CustomSignUp(string id, string pw)
    {
        Debug.Log("회원가입을 요청합니다.");

        var bro = Backend.BMember.CustomSignUp(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("회원가입에 성공하였습니다. : " + bro);
            Debug.Log("로그인을 시도합니다.");
            CustomLogin(id, pw);
            return 0;
        }
        else
        {
            Debug.LogError("회원가입에 실패하였습니다. : " + bro);
        }
        return bro.StatusCode;
    }

    public int CustomLogin(string id, string pw)
    {
        Debug.Log("로그인을 요청합니다.");

        var bro = Backend.BMember.CustomLogin(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("로그인이 성공했습니다. : " + bro);
            return 0;
        }
        else if (bro.StatusCode == 401)
        {
            Debug.LogError("회원가입을 시도합니다.");
            return CustomSignUp(id, pw);
        }
        else
        {
            Debug.LogError("로그인이 실패했습니다. : " + bro);
        }
        return bro.StatusCode;
    }

    public int UpdateNickname(string nickname)
    {
        Debug.Log("닉네임 변경을 요청합니다.");

        var bro = Backend.BMember.UpdateNickname(nickname);

        if (bro.IsSuccess())
        {
            Debug.Log("닉네임 변경에 성공했습니다 : " + bro);
        }
        else
        {
            Debug.LogError("닉네임 변경에 실패했습니다 : " + bro);
        }

        return bro.StatusCode;
    }

    public void CreateNickname()
    {
        int random = Random.Range(10000, 99999);
        string nickname = "익명" + random;
        UpdateNickname(nickname);
    }
}
