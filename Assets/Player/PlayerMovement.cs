using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어의 움직임을 구현
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 100;      //플레이어의 이동속도
    [SerializeField] float dashSpeed = 20;  //플레이어 대쉬속도
    [SerializeField] float xRange = 59;     //x 값 범위
    [SerializeField] float yRange = 59;  //y 값 범위

    Vector2 moveInput;  //플레이어 인풋시스템
    bool hasDash = true;
    float timer = 0f;

    public static PlayerMovement Instance { get; private set; }
    Rigidbody2D playerRigidbody;
    Collider2D playerCollider;
    Animator animator;
    [SerializeField] Transform playerFront;

    void Awake()
    {
        Instance = this;
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        playerRigidbody.drag = 8;
        transform.position = Vector2.zero;
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.angularVelocity = 0;

        animator.SetBool("isDead", false);
    }

    //플레이어의 움직임 조작 메서드
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        //플레이어 조작 시 캐릭터 방향을 변경
        if (moveInput == Vector2.zero) { return; }
        float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    //플레이어 대쉬 메서드
    void OnDash(InputValue value)
    {
        if (value.isPressed && hasDash)
        {
            Vector2 playerDirection = (playerFront.position - transform.position).normalized;
            playerRigidbody.AddForce(playerDirection * dashSpeed, ForceMode2D.Impulse);
            playerCollider.enabled = false;
            hasDash = false;
        }
    }

    void FixedUpdate()
    {
        Move();

        //대쉬 소모 시 일정 시간동안 메서드 실행
        if (!hasDash)
        {
            timer += Time.deltaTime;

            if (timer >= 0.5f) { playerCollider.enabled = true; }
            
            if (timer >= 5f) { DashCooltime(); }
        }
    }

    /// <summary>
    /// 플레이어를 움직이도록 하는 메서드
    /// </summary>
    void Move()
    {
        playerRigidbody.AddForce(moveInput * speed);

        //플레이어 x값을 -11 ~ 11, y > -7 로 지정
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -xRange, xRange);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -yRange, yRange);

        transform.position = clampedPosition;
    }

    void DashCooltime()
    {
        hasDash = true;
        timer = 0f;
    }
}
