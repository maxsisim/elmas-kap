using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthBar;
    public GameObject deathMenu;

    public AudioSource hurtSound;
    public AudioSource deathSound;

    private bool isDead = false;
    public float healInterval = 5f;
    public int healAmount = 10;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (deathMenu != null)
        {
            deathMenu.SetActive(false);
        }

        StartCoroutine(HealOverTime());
    }

    public void TakeDamage(int damage, bool playHurtSound = true)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;

            if (healthBar != null)
                healthBar.value = currentHealth;

            Die();
            return;
        }

        if (playHurtSound && hurtSound != null && !hurtSound.isPlaying)
        {
            hurtSound.Play();
        }

        if (healthBar != null)
            healthBar.value = currentHealth;
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " öldü!");

        if (deathSound != null)
            deathSound.Play();

        if (deathMenu != null)
            deathMenu.SetActive(true);

        Time.timeScale = 0f;
    }

    public bool IsDead()
    {
        return isDead;
    }

    private IEnumerator HealOverTime()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(healInterval);

            if (currentHealth < maxHealth)
            {
                currentHealth += healAmount;
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;

                if (healthBar != null)
                    healthBar.value = currentHealth;
            }
        }
    }
}
