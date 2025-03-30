using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StaticObstacle : ObstaclePositioner
{
    [SerializeField] string obstacleName;
    [SerializeField] string sortingLayer;
    [SerializeField] Vector3 defaultScale;

    Transform shadow;
    CircleCollider2D thisCollider;
    SpriteRenderer tireSprite;
    SpriteRenderer shadowSprite;

    protected override void Start()
    {
        base.Start();
        gameObject.name = obstacleName;
        
        shadow = transform.GetChild(0);
        thisCollider = GetComponent<CircleCollider2D>();
        tireSprite = GetComponent<SpriteRenderer>();
        shadowSprite = shadow.GetComponent<SpriteRenderer>();

        SetInsidePosition();

        Sequence dropSequence = DOTween.Sequence();
        dropSequence.Append(shadowSprite.DOFade(0.7f, 1f).SetEase(Ease.OutSine));
        dropSequence.Append(transform.DOScale(defaultScale, 2f).SetEase(Ease.OutBounce)
        .OnComplete(() =>
        {
            shadow.gameObject.SetActive(false);
            thisCollider.enabled = true;
            tireSprite.sortingLayerName = sortingLayer;
        }));
        dropSequence.Join(tireSprite.DOFade(1f, 1f).SetEase(Ease.OutSine));
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Area")) { Destroy(gameObject); }
    }
}
