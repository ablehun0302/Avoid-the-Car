using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System.Text;

public class UserData
{
    public int maxScore = 0;
    public int money = 0;
    public Dictionary<string, int> deadBy = new Dictionary<string, int>();

    // 데이터를 디버깅하기 위한 함수입니다.(Debug.Log(UserData);)
    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine($"maxScore : {maxScore}");
        result.AppendLine($"money : {money}");

        result.AppendLine($"dead by");
        foreach (var itemKey in deadBy.Keys)
        {
            result.AppendLine($"| {itemKey} : {deadBy[itemKey]}번");
        }

        return result.ToString();
    }
}

public class BackendGameData
{
    private static BackendGameData _instance = null;

    public static BackendGameData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendGameData();
            }

            return _instance;
        }
    }

    public static UserData userData;

    private string gameDataRowInDate = string.Empty;

    public void GameDataInsert()
    {
        if (userData == null)
        {
            userData = new UserData();
        }

        Debug.Log("데이터를 초기화합니다.");
        userData.maxScore = 0;
        userData.money = 0;

        userData.deadBy.Add("하얀차", 0);
        userData.deadBy.Add("파란차", 0);
        userData.deadBy.Add("구급차", 0);
        userData.deadBy.Add("사람", 0);

        Debug.Log("뒤끝 업데이트 목록에 해당 데이터들을 추가합니다.");
        Param param = new Param();
        param.Add("maxScore", userData.maxScore);
        param.Add("money", userData.money);
        param.Add("deadBy", userData.deadBy);


        Debug.Log("게임 정보 데이터 삽입을 요청합니다.");
        var bro = Backend.GameData.Insert("USER_DATA", param);

        if (bro.IsSuccess())
        {
            Debug.Log("게임 정보 데이터 삽입에 성공했습니다. : " + bro);

            //삽입한 게임 정보의 고유값입니다.  
            gameDataRowInDate = bro.GetInDate();
        }
        else
        {
            Debug.LogError("게임 정보 데이터 삽입에 실패했습니다. : " + bro);
        }
    }

    public void GameDataGet()
    {
        Debug.Log("게임 정보 조회 함수를 호출합니다.");

        var bro = Backend.GameData.GetMyData("USER_DATA", new Where());

        if (bro.IsSuccess())
        {
            Debug.Log("게임 정보 조회에 성공했습니다. : " + bro);


            LitJson.JsonData gameDataJson = bro.FlattenRows(); // Json으로 리턴된 데이터를 받아옵니다.  

            // 받아온 데이터의 갯수가 0이라면 데이터가 존재하지 않는 것입니다.  
            if (gameDataJson.Count <= 0)
            {
                Debug.LogWarning("데이터가 존재하지 않습니다.");
            }
            else
            {
                gameDataRowInDate = gameDataJson[0]["inDate"].ToString(); //불러온 게임 정보의 고유값입니다.  

                userData = new UserData();

                userData.maxScore = int.Parse(gameDataJson[0]["maxScore"].ToString());
                userData.money = int.Parse(gameDataJson[0]["money"].ToString());

                foreach (string key in gameDataJson[0]["deadBy"].Keys)
                {
                    userData.deadBy.Add(key, int.Parse(gameDataJson[0]["deadBy"][key].ToString()));
                }

                Debug.Log(userData.ToString());
            }
        }
        else
        {
            Debug.LogError("게임 정보 조회에 실패했습니다. : " + bro);
        }
    }

    /// <summary>
    /// 유저 데이터를 설정하는 메서드
    /// </summary>
    /// <param name="score">점수</param>
    /// <param name="money">돈</param>
    /// <param name="something">부딛힌 장애물</param>
    public void UserDataSet(int score, int money, string something)
    {
        Debug.Log("점수 설정");
        if (userData.maxScore < score) { userData.maxScore = score; }
        userData.money += money;
        userData.deadBy[something] ++;
    }

    public void GameDataUpdate()
    {
        if (userData == null)
        {
            Debug.LogError("서버에서 다운받거나 새로 삽입한 데이터가 존재하지 않습니다. Insert 혹은 Get을 통해 데이터를 생성해주세요.");
            return;
        }

        Param param = new Param();
        param.Add("maxScore", userData.maxScore);
        param.Add("money", userData.money);
        param.Add("deadBy", userData.deadBy);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(gameDataRowInDate))
        {
            Debug.Log("내 제일 최신 게임 정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }
        else
        {
            Debug.Log($"{gameDataRowInDate}의 게임 정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.UpdateV2("USER_DATA", gameDataRowInDate, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("게임 정보 데이터 수정에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("게임 정보 데이터 수정에 실패했습니다. : " + bro);
        }
    }
}
