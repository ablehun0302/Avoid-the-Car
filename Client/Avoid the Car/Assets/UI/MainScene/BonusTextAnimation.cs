using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BonusTextAnimation : MonoBehaviour
{
    void Start()
    {
        TextMeshPro tmp = GetComponent<TextMeshPro>();
        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InExpo))
                  .AppendInterval(0.2f)
                  .Append(tmp.DOFade(0, 1f).SetEase(Ease.OutSine).OnComplete(() => Destroy(gameObject)));  
    }
}
