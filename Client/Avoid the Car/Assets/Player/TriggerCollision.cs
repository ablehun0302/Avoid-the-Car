using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TriggerCollision : MonoBehaviour
{
    int itemCoolTime = 8;

    GameManager gameManager;
    Rigidbody2D myRigidbody;
    DeathCollision deathCollision;
    Collider2D deathCollider;
    [SerializeField] ParticleSystem itemVFX;
    [SerializeField] PhysicsMaterial2D noBounce;
    [SerializeField] PhysicsMaterial2D playerBounce;
    [Header("아이템 효과 화면 관련")]
    [SerializeField] GameObject miscGroup;
    [SerializeField] Image miscImage;

    void Start()
    {
        gameManager = GameManager.Instance;
        myRigidbody = transform.root.GetComponent<Rigidbody2D>();

        deathCollision = transform.parent.GetComponentInChildren<DeathCollision>();
        deathCollider = deathCollision.GetComponent<Collider2D>();
    }

  void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Item") || gameManager.IsGameOver) return;

        deathCollision.IsInvulnerable = true;
        myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        deathCollider.sharedMaterial = noBounce;

        miscGroup.SetActive(true);
        itemVFX.Play();
        BackendGameLog.Instance.ItemUseCount ++;

        StopCoroutine(InvulnerabilityRoutine());
        StartCoroutine(InvulnerabilityRoutine());

        Destroy(other.gameObject);
    }

    IEnumerator InvulnerabilityRoutine()
    {
        float timer = 0f;

        while (timer <= itemCoolTime)
        {
            timer += Time.deltaTime;

            miscImage.fillAmount = 1 - (timer / itemCoolTime) ;

            yield return null;
        }

        myRigidbody.constraints = RigidbodyConstraints2D.None;
        deathCollider.sharedMaterial = playerBounce;
        miscGroup.SetActive(false);
        itemVFX.Stop();
        deathCollision.IsInvulnerable = false;
    }
}
