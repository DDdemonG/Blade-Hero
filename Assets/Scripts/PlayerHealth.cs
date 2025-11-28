using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

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
        if (isInvincible) return;

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
}
