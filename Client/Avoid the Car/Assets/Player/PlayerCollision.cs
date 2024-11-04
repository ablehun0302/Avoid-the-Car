using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어 충돌과 이후 이벤트를 구현
/// </summary>
public class PlayerCollision : MonoBehaviour
{
    bool cheat = false;         //치트 변수 - 무적상태로 변함

    Animator animator;          //플레이어 텍스쳐 변환용 애니메이터
    ScoreManager scoreManager;
    GameManager gameManager;
    [SerializeField] GameObject explosionVFX;   //폭파 파티클
    [SerializeField] GameObject hitVFX;         //부딪힘 파티클

    void Start()
    {
        animator = GetComponent<Animator>();
        scoreManager = ScoreManager.Instance;
        gameManager = GameManager.Instance;
    }

    /*void OnCheat(InputValue value)
    {
        if(value.isPressed)
        {
            cheat = !cheat;
        }
        Debug.Log(cheat);
    }*/

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Obstacle") || gameManager.IsGameOver || cheat) { return; }
        
        //게임오버 동작 실행
        animator.SetBool("isDead", true);
        gameManager.GameOver();
        //Debug.Log("플레이어 충돌" + gameManager.IsGameOver);

        //부딛힌 좌표
        ContactPoint2D contact = other.contacts[0];
        Vector2 collisionPos = contact.point;

        string obstacleName = other.gameObject.name;
                
        switch (obstacleName)   //파티클 생성
        {
            case "사람":
                Instantiate(hitVFX, collisionPos, hitVFX.transform.rotation);
                break;
            default:
                Instantiate(explosionVFX, collisionPos, explosionVFX.transform.rotation);
                break;
        }

        //유저 점수 로그 입력
        if ( BackendGameData.userData == null ) { return; }
        BackendGameLog.Instance.DeadLogInsert(scoreManager.Score, obstacleName);

        //유저 데이터 수정
        BackendGameData.Instance.UserDataSet(scoreManager.Score, 0, obstacleName);
    }
}