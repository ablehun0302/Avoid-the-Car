using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameOverTextAnim : MonoBehaviour
{
    [SerializeField] float delayTime = 0f;
    [SerializeField] Vector3 firstScale;

    void OnEnable()
    {
        transform.localScale = Vector3.zero;
        Sequence mySequence = DOTween.Sequence();
        mySequence.AppendInterval(delayTime)
                  .AppendCallback(()=>{ transform.localScale = firstScale; })
                  .Append(transform.DOScale(Vector3.one, 1.5f).SetEase(Ease.OutBounce));
    }
}
