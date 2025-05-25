using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    public void DealDamage() // Animasyon eventi burayý çaðýrýr
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (hitPlayer != null)
        {
            // Player nesnesinde TakeDamage(int) metodunu çaðýrmayý dene
            hitPlayer.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
