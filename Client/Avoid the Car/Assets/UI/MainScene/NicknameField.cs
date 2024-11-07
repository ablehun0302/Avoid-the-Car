using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using TMPro;

public class NicknameField : MonoBehaviour
{
    TMP_InputField nicknameInput;
    [SerializeField] TextMeshProUGUI warningText;

    void Start()
    {
        nicknameInput = GetComponent<TMP_InputField>();

        Backend.BMember.GetUserInfo(callback =>
        {
            string nickname = callback.GetReturnValuetoJSON()["row"]["nickname"].ToString();
            nicknameInput.text = nickname;
        });

        if (nicknameInput.text == "" || nicknameInput.text == null)
        {
            BackendLogin.Instance.CreateNickname();
            Backend.BMember.GetUserInfo(callback =>
            {
                string nickname = callback.GetReturnValuetoJSON()["row"]["nickname"].ToString();
                nicknameInput.text = nickname;
            });
        }

    }

    void OnEnable()
    {
        warningText.text = "";
    }

    public void UpdateNicknameButton()
    {
        int status = BackendLogin.Instance.UpdateNickname(nicknameInput.text);

        switch (status)
        {
            case 204:
                warningText.text = "*닉네임 변경에 선공하였습니다!";
                break;
            case 409:
                warningText.text = "*중복된 닉네임 입니다. 다른 닉네임으로 설정해주세요.";
                break;
            default:
                warningText.text = "*닉네임 변경에 실패하였습니다. 형식에 맞게 설정해주세요.";
                break;
        }
    }
}
