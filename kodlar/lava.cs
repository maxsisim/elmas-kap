using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Lava'ya d��t�n!");

            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                // PlayerHealth'in IsDead() fonksiyonu ile �l� olup olmad���n� kontrol et
                if (!health.IsDead()) // E�er �l� de�ilse, hasar al
                {
                    health.TakeDamage(health.maxHealth); // Oyuncuyu direkt �ld�r
                }
            }
        }
    }
}
