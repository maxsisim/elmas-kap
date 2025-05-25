using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthBar;
    public AudioSource hurtSound;
    public AudioSource deathSound;

    private bool isDead = false;

    // Skor yöneticisine referans (opsiyonel)
    private EnemySpawnerManager spawnerManager;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        // Skor sistemi varsa otomatik bulur
        spawnerManager = FindObjectOfType<EnemySpawnerManager>();
    }

    public void TakeDamage(int damage, bool playSound)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;

            if (healthBar != null)
            {
                healthBar.value = currentHealth;
            }

            Die(); // Sadece ölüm sesi çalınır
            return;
        }

        // Ölmediyse hasar sesi çalınır
        if (playSound && hurtSound != null)
        {
            hurtSound.Play();
        }

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " öldü!");

        // Eğer skor yöneticisi varsa, skoru artır
        if (spawnerManager != null)
        {
            spawnerManager.AddScore(1);
        }

        if (deathSound != null && deathSound.clip != null)
        {
            deathSound.Play();
            Destroy(gameObject, deathSound.clip.length);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
