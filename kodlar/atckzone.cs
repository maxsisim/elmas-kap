using UnityEngine;

public class AttackZone : MonoBehaviour
{
    public EnemySpawnerManager spawnerManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && enemy.IsDead)
            {
                if (spawnerManager != null)
                    spawnerManager.AddScore(1);

                Destroy(enemy.gameObject);
            }
        }
    }
}
