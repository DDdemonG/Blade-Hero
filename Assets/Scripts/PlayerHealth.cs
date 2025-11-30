using UnityEngine;
using System.Collections;
using static Codice.CM.Common.CmCallContext;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;
    private bool hasShield = false;

    public float invincibilityDuration = 1.0f;
    public float flashDuration = 0.1f; 
    public ParticleSystem hitParticle;

    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (hitParticle != null)
        {
            hitParticle.Stop();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        if (isInvincible) return;
        if (hasShield)
        {
            hasShield = false;
            Debug.Log("Defended once");
            StartCoroutine(InvincibilityRoutine());
            return; 
        }
        currentHealth -= damage;
        Debug.Log("oooooof! Vie restant: " + currentHealth);

        if (hitParticle != null)
        {
            hitParticle.Play();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityRoutine());
        }
    }

    public void Heal(int amount)
    {
        if (isDead) return;
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        Debug.Log("Heal! HP now: " + currentHealth);
    }

    void Die()
    {
        Debug.Log("Player dead");
        spriteRenderer.enabled = false;
        enabled = false;
    }

    IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;

        float endTime = Time.time + invincibilityDuration;

        while (Time.time < endTime)
        {
            spriteRenderer.color = new Color(255f, 124f, 124f, 255f);
            yield return new WaitForSeconds(flashDuration);

            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(flashDuration);
        }

        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        isInvincible = false;
    }

    public void AddShield()
    {
        hasShield = true;
        Debug.Log("Get Shield!");
    }
}
