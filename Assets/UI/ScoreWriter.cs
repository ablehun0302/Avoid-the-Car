using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreWriter : MonoBehaviour
{
    [Header("점수와 함께 적을 텍스트 (점수는 '{0}' 으로 기입)")]
    [SerializeField] string sourceText;

    TextMeshProUGUI scoreText;
    ScoreManager scoreManager;

    void OnEnable()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreManager = ScoreManager.Instance;
    }

    void Update()
    {
        scoreText.SetText(sourceText, scoreManager.Score);
    }
}
