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
    [SerializeField] float xRange = 20;     //x 값 범위
    [SerializeField] float yRange = 20;  //y 값 범위

    Vector2 moveInput;  //플레이어 인풋시스템

    public static PlayerMovement Instance { get; private set; }
    Rigidbody2D playerRigidbody;

    void Awake()
    {
        Instance = this;
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        playerRigidbody.drag = 8;
        transform.position = Vector2.zero;
        playerRigidbody.velocity = Vector2.zero;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 플레이어를 움직이도록 하는 메서드
    /// </summary>
    void Move()
    {
        /*Vector3 moveTo = moveInput * Time.deltaTime * speed;
        transform.Translate(moveTo);*/
        playerRigidbody.AddForce(moveInput * speed);

        //플레이어 x값을 -11 ~ 11, y > -7 로 지정
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -xRange, xRange);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -yRange, yRange);

        transform.position = clampedPosition;
    }
}
