using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MaxScoreWriter : MonoBehaviour
{
    TextMeshProUGUI maxScoreText;

    void OnEnable()
    {
        maxScoreText = GetComponent<TextMeshProUGUI>();
        maxScoreText.text = "당신의 최고 점수: " + BackendGameData.userData.maxScore;
    }
}
