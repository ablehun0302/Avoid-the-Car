using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 타일맵을 무한히 확장하는 것을 구현
/// </summary>
public class TileExpander : MonoBehaviour
{
    //[SerializeField] GameObject player;
    [SerializeField] int tileHeight = 20;
    PlayerMovement player;

    void Start()
    {
        player = PlayerMovement.Instance;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Area") || !enabled) { return; }

        float tileMoveDirection = Mathf.Sign(player.transform.position.y - transform.position.y);

        transform.Translate(Vector3.up * tileMoveDirection * tileHeight * 2);
    }
}
