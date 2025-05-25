using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement & Detection")]
    public float followSpeed = 0.7f;
    public float detectionRange = 5f;
    public float attackRange = 0.4f;

    [Header("Attack Settings")]
    public int damage = 10;
    public float attackCooldown = 1.5f;
    public Transform attackPoint;

    private Transform target;
    private Animator anim;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    private bool isAttackOnCooldown = false;
    private bool playerDetected = false;
    private Collider2D attackCollider;
    private GameObject targetPlayerInRange = null;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (attackPoint != null)
        {
            attackCollider = attackPoint.GetComponent<Collider2D>();
            if (attackCollider != null)
            {
                attackCollider.isTrigger = true;
            }
        }
    }

    void Update()
    {
        DetectPlayer();

        if (playerDetected)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);

            if (distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer();
                anim.SetBool("isWalk", true);
                anim.SetBool("isAttack", false);
            }
            else
            {
                anim.SetBool("isWalk", false);
                if (!isAttackOnCooldown)
                {
                    StartCoroutine(AttackPlayer());
                }
            }
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }

    void DetectPlayer()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        playerDetected = distance <= detectionRange;
    }

    void MoveTowardsPlayer()
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), followSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);

        sprite.flipX = (target.position.x < transform.position.x);
    }

    IEnumerator AttackPlayer()
    {
        isAttackOnCooldown = true;
        anim.SetBool("isAttack", true);

        yield return new WaitForSeconds(attackCooldown);

        isAttackOnCooldown = false;
        anim.SetBool("isAttack", false);
    }

    // Bu fonksiyon animasyonun vuruş anında Animation Event olarak çağrılır
    public void DealDamage()
    {
        if (targetPlayerInRange != null)
        {
            PlayerHealth health = targetPlayerInRange.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage, true);
            }
        }
    }

    // Trigger'a giren oyuncuyu takip et
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetPlayerInRange = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && targetPlayerInRange == other.gameObject)
        {
            targetPlayerInRange = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, attackPoint.localScale);
    }
}
