using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    public int maxHealth = 100;
    private int currentHealth;

    // Bu özellik, dışarıdan düşmanın ölüp ölmediğini kontrol etmek için
    public bool IsDead { get; private set; } = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        currentHealth -= damage;

        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        IsDead = true;

        anim.SetBool("IsDead", true);
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;

        // Skor ekle
        EnemySpawnerManager manager = FindObjectOfType<EnemySpawnerManager>();
        if (manager != null)
        {
            manager.AddScore(1);
        }

        Destroy(gameObject, 2f);
    }
}
