using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirstRankWriter : MonoBehaviour
{
    TextMeshProUGUI firstRankText;
    [Header("{0}은 이름, {1}은 점수")]
    [TextArea] [SerializeField] string sourceText;

    void Awake()
    {
        firstRankText = GetComponent<TextMeshProUGUI>();
    }
    
    void OnEnable()
    {
        List<string> firstRankInfo = BackendRank.Instance.FirstRankGet();
        
        if (firstRankInfo != null && firstRankInfo.Count >= 2)
        {
            string playerName = firstRankInfo[0]; // 첫 번째 요소
            string playerScore = firstRankInfo[1]; // 두 번째 요소

            firstRankText.text = string.Format(sourceText, playerName, playerScore);
        }
        else
        {
            firstRankText.text = "랭킹 정보를 불러올 수 없습니다.";
        }
    }
}
