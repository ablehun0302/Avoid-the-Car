using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

/// <summary>
/// 플레이어 충돌과 이후 이벤트를 구현
/// </summary>
public class PlayerCollision : MonoBehaviour
{
    PlayerMovement playerMovement;
    GameObject groundGrid;
    GameObject followCamera;

    void Start()
    {
        playerMovement = PlayerMovement.Instance;
        groundGrid = GameObject.Find("GroundGrid");
        followCamera = GameObject.Find("FollowCamera");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Obstacle")) { return; }
        
        //장애물에 부딛힐 시 플레이어 움직임 정지
        playerMovement.enabled = false;
        
        //타일 확장 중지
        for (int i = 0; i < groundGrid.transform.childCount; i++)
        {
            Transform tile = groundGrid.transform.GetChild(i);
            tile.gameObject.GetComponent<TileExpander>().enabled = false;
        }

        //카메라 비활성화
        followCamera.GetComponent<CinemachineVirtualCamera>().enabled = false;
    }
}
