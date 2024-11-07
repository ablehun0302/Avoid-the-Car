using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirstRankWriter : MonoBehaviour
{
    TextMeshProUGUI firstRankText;

    void Awake()
    {
        firstRankText = GetComponent<TextMeshProUGUI>();
    }
    void OnEnable()
    {
        firstRankText.text = BackendRank.Instance.FirstRankGet();
    }
}
