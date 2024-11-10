using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// 플레이어의 움직임을 구현
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;  //플레이어 인풋시스템
    [SerializeField] float speed = 100;      //플레이어의 이동속도
    [SerializeField] float dashSpeed = 20;  //플레이어 대쉬속도
    [SerializeField] float xRange = 59;     //x 값 범위
    [SerializeField] float yRange = 59;  //y 값 범위

    bool hasDash = true;
    float timer = 0f;
    float dashInvulnerableTime = 0.5f;
    float dashCooldownTime = 5;
    Color waitingColor = new Color(1, 1, 1, 0.5f);

    public static PlayerMovement Instance { get; private set; }
    Rigidbody2D playerRigidbody;
    Collider2D playerCollider;
    Animator animator;
    [SerializeField] Transform playerFront;
    [SerializeField] Image cooldownImage;

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
        DashRestore();

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
            cooldownImage.color = waitingColor;
            hasDash = false;
            BackendGameLog.Instance.DashCount ++;
        }
    }

    void FixedUpdate()
    {
        Move();

        //대쉬 소모 시
        if (!hasDash)
        {
            timer += Time.deltaTime;

            if (timer >= dashInvulnerableTime) { playerCollider.enabled = true; }
            
            if (timer >= dashCooldownTime) { DashRestore(); }

            if (timer == 0) { cooldownImage.fillAmount = 1; }
            else { cooldownImage.fillAmount = timer / dashCooldownTime; }
        }
    }

    /// <summary>
    /// 플레이어를 움직이도록 하는 메서드
    /// </summary>
    void Move()
    {
        playerRigidbody.AddForce(moveInput * speed);

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -xRange, xRange);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -yRange, yRange);

        transform.position = clampedPosition;
    }

    void DashRestore()
    {
        hasDash = true;
        timer = 0f;
        cooldownImage.fillAmount = 1;
        cooldownImage.color = Color.white;
    }
}
