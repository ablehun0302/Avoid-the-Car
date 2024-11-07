using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using TMPro;

public class NicknameField : MonoBehaviour
{
    TMP_InputField nicknameInput;

    void Start()
    {
        nicknameInput = GetComponent<TMP_InputField>();

        Backend.BMember.GetUserInfo(callback =>
        {
            string nickname = callback.GetReturnValuetoJSON()["row"]["nickname"].ToString();
            nicknameInput.text = nickname;
        });
    }
}
