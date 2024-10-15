using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장애물의 움직임을 구현
/// </summary>
public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float xRange = 18;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);

        // 화면을 벗어나면 없애기
        if (transform.position.x >= xRange)
        {
            Destroy(gameObject);
        }
    }
}
