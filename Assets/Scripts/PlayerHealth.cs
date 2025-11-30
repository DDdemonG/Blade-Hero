using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D col;

    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;
    private bool hasShield = false;

    public float invincibilityDuration = 1.0f;
    public float flashDuration = 0.1f; 
    public ParticleSystem hitParticle;

    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;
    public GameObject shield;
    private AudioSource audioSource;
    public AudioClip shieldBreakSound;
    public AudioClip getHitSound;

    public GameObject heartContainer;
    private HeartContainerManager heartManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (shield != null) shield.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        heartManager = heartContainer.GetComponent<HeartContainerManager>();
        heartManager.InitHearts(maxHealth);
    }

    void Start()
    {
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
            shield.SetActive(hasShield);
            audioSource.PlayOneShot(shieldBreakSound, 0.1f);
            Debug.Log("Defended once");
            StartCoroutine(InvincibilityRoutine());
            return; 
        }
        currentHealth -= damage;
        Debug.Log("oooooof! Vie restant: " + currentHealth);
        audioSource.PlayOneShot(getHitSound, 0.5f);
        heartManager.UpdateHearts(currentHealth);

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
        if (isDead) return;
        isDead = true;

        Debug.Log("Player dead");
        isInvincible = false;

        if (rb != null)
            rb.linearVelocity = Vector2.zero;
      
        if (col != null)
            col.enabled = false;
       
        PlayerController pc = GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.isDead = true;
        }

        
        if (animator != null)
        {
            animator.SetTrigger("Die");
            animator.Play("DIE", -1, 0f); 
        }

        Invoke("DisablePlayer", 1.5f);
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
        shield.SetActive(hasShield);
        Debug.Log("Get Shield!");
    }
    void DisablePlayer()
    {
        Debug.Log("GAME OVER");
        Time.timeScale = 0f;
    }

}
