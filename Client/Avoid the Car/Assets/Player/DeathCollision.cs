using UnityEngine;

/// <summary>
/// 플레이어 사망 감지를 구현
/// </summary>
public class DeathCollision : MonoBehaviour
{
    public bool IsInvulnerable { get; set; } = false;

    GameManager gameManager;
    ScoreManager scoreManager;
    [SerializeField] Animator animator;
    [SerializeField] GameObject explosionVFX;   //폭파 파티클
    [SerializeField] GameObject hitVFX;         //부딪힘 파티클

    void Start()
    {
        gameManager = GameManager.Instance;
        scoreManager = gameManager.GetScoreManager();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Obstacle") || IsInvulnerable || gameManager.IsGameOver) { return; }
        
        //게임오버 동작 실행
        animator.SetBool("isDead", true);
        gameManager.GameOver();
        //Debug.Log("플레이어 충돌" + gameManager.IsGameOver);

        //부딛힌 좌표에 파티클 생성
        ContactPoint2D contact = other.contacts[0];
        Vector2 collisionPos = contact.point;

        string obstacleName = other.gameObject.name;
                
        switch (obstacleName)   //파티클 생성
        {
            case "타이어":
                Instantiate(hitVFX, collisionPos, hitVFX.transform.rotation);
                break;
            default:
                Instantiate(explosionVFX, collisionPos, explosionVFX.transform.rotation);
                break;
        }

        //유저 점수 로그 입력
        if ( BackendGameData.userData == null ) { return; }
        BackendGameLog.Instance.DeadLogInsert(obstacleName);

        //유저 데이터 수정
        BackendGameData.Instance.UserDataSet(scoreManager.Score, 0, obstacleName);
    }
}