using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System.Text;
using System.Linq;

public class BackendRank
{
    private static BackendRank _instance = null;

    public static BackendRank Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendRank();
            }

            return _instance;
        }
    }

    public void RankInsert(int score)
    {
        string rankUUID = "0193069c-f85c-732c-bdb5-4934d89d9245"; //리더보드 UUID값

        string tableName = "USER_DATA";
        string rowInDate = string.Empty;

        // 랭킹을 삽입하기 위해서는 게임 데이터에서 사용하는 데이터의 inDate값이 필요합니다.  
        // 따라서 데이터를 불러온 후, 해당 데이터의 inDate값을 추출하는 작업을 해야합니다.  
        Debug.Log("데이터 조회를 시도합니다.");
        var bro = Backend.GameData.GetMyData(tableName, new Where());

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("데이터 조회 중 문제가 발생했습니다 : " + bro);
            return;
        }

        Debug.Log("데이터 조회에 성공했습니다 : " + bro);

        if (bro.FlattenRows().Count > 0)
        {
            rowInDate = bro.FlattenRows()[0]["inDate"].ToString();
        }
        else
        {
            Debug.Log("데이터가 존재하지 않습니다. 데이터 삽입을 시도합니다.");
            var bro2 = Backend.GameData.Insert(tableName);

            if (bro2.IsSuccess() == false)
            {
                Debug.LogError("데이터 삽입 중 문제가 발생했습니다 : " + bro2);
                return;
            }

            Debug.Log("데이터 삽입에 성공했습니다 : " + bro2);

            rowInDate = bro2.GetInDate();
        }

        Debug.Log("내 게임 정보의 rowInDate : " + rowInDate); // 추출된 rowIndate의 값은 다음과 같습니다.  

        Param param = new Param();
        param.Add("maxScore", score);

        // 추출된 rowIndate를 가진 데이터에 param값으로 수정을 진행하고 랭킹에 데이터를 업데이트합니다.  
        Debug.Log("랭킹 삽입을 시도합니다.");
        Backend.URank.User.UpdateUserScore(rankUUID, tableName, rowInDate, param, callback =>
        {
            if (callback.IsSuccess() == false)
            {
                Debug.LogError("랭킹 등록 중 오류가 발생했습니다. : " + callback);
                return;
            }

            Debug.Log("랭킹 삽입에 성공했습니다. : " + callback);
        });
    }

    public List<string> FirstRankGet()
    {
        string rankUUID = "0193069c-f85c-732c-bdb5-4934d89d9245"; //리더보드 UUID값
        var bro = Backend.URank.User.GetRankList(rankUUID);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("랭킹 조회중 오류가 발생했습니다. : " + bro);
            return null;
        }

        Debug.Log("랭킹 조회에 성공했습니다. : " + bro);

        Debug.Log("총 랭킹 등록 유저 수 : " + bro.GetFlattenJSON()["totalCount"].ToString());

        var jsonData = bro.FlattenRows()[0];
        List<string> firstRankInfo = new List<string>
        {
            jsonData["nickname"].ToString(),
            jsonData["score"].ToString()
        };

        return firstRankInfo;
    }

    public List<List<string>> RankListGet()
    {
        string rankUUID = "0193069c-f85c-732c-bdb5-4934d89d9245"; //리더보드 UUID값
        var bro = Backend.URank.User.GetRankList(rankUUID);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("랭킹 조회중 오류가 발생했습니다. : " + bro);
            return null;
        }

        Debug.Log("랭킹 조회에 성공했습니다. : " + bro);

        Debug.Log("총 랭킹 등록 유저 수 : " + bro.GetFlattenJSON()["totalCount"].ToString());

        List<List<string>> rankList = new List<List<string>>();     
        foreach (LitJson.JsonData jsonData in bro.FlattenRows())
        {
            List<string> rankInfo = new List<string>
            {
                jsonData.ContainsKey("rank") ? jsonData["rank"].ToString() + "등" : "Unknown",
                jsonData.ContainsKey("nickname") ? jsonData["nickname"].ToString() : "Unknown",
                jsonData.ContainsKey("score") ? jsonData["score"].ToString() + "점" : "0점"
            };
            
            rankList.Add(rankInfo);
        }
        return rankList;
    }
}
