using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어의 움직임을 구현
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10;      //플레이어의 이동속도
    [SerializeField] float xRange = 11;     //x 값 범위
    [SerializeField] float minYRange = -7;  // -y 값 범위
    [SerializeField] float maxYRange = 100; // +y 값 범위

    Vector2 moveInput;  //플레이어 인풋시스템

    public static PlayerMovement Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Update()
    {
        Move();
    }

    /// <summary>
    /// 플레이어를 움직이도록 하는 메서드
    /// </summary>
    void Move()
    {
        Vector3 moveTo = moveInput * Time.deltaTime * speed;
        transform.Translate(moveTo);

        //플레이어 x값을 -11 ~ 11, y > -7 로 지정
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -xRange, xRange);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minYRange, maxYRange);

        transform.position = clampedPosition;
    }
}
