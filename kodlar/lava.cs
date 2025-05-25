using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Lava'ya düþtün!");

            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                // PlayerHealth'in IsDead() fonksiyonu ile ölü olup olmadýðýný kontrol et
                if (!health.IsDead()) // Eðer ölü deðilse, hasar al
                {
                    health.TakeDamage(health.maxHealth); // Oyuncuyu direkt öldür
                }
            }
        }
    }
}
