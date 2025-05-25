using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int damage = 20;
    public LayerMask enemyLayer;

    public AudioSource attackSound;

    public void DealDamage()
    {
        // 🔊 Saldırı sesi sadece animasyon eventiyle tetiklenirse çal
        if (attackSound != null)
        {
            attackSound.Play();
        }

        // Birden fazla düşmana vurmak istersen:
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                // ✅ "true" diyerek sadece Player saldırısında ses oynatılacak
                enemyHealth.TakeDamage(damage, true);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
