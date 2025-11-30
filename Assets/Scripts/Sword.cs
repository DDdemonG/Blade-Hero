using UnityEngine;

public class Sword : MonoBehaviour
{
    public int level = 0;

    private AudioSource audioSource;

    private PlayerHealth playerHP;

    public ParticleSystem clashEffectPrefab;

    private float nextAttackTime = 0f;
    public float attackCooldown = 0.2f;

    private int damage;

    private void Awake()
    {
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
        damage = (level + 1) * 10;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("PlayerBlade")) { 
            Sword otherSword = other.GetComponent<Sword>();
            if (otherSword != null)
            {
                if (!gameObject.activeInHierarchy || !otherSword.gameObject.activeInHierarchy)
                    return;

                if (other.CompareTag("PlayerBlade")) return;

                if (Time.time < nextAttackTime) return;

                PlayClashEffect(other);

                if (level > otherSword.level)
                {
                    PlayAndDestroy(otherSword.gameObject);
                    nextAttackTime = Time.time + attackCooldown;
                }
                else if (level < otherSword.level)
                {
                    PlayAndDestroy(gameObject);
                }
                else
                {
                    PlayAndDestroy(otherSword.gameObject);
                    PlayAndDestroy(gameObject);
                }

                return;
            }
        }

        if (gameObject.CompareTag("PlayerBlade") ) {
            EnemyController enemy =
                other.GetComponent<EnemyController>() ??
                other.GetComponentInParent<EnemyController>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                return;
            }
        }

        if (gameObject.CompareTag("EnemyBlade") && other.gameObject.CompareTag("Player"))
        {
            if (playerHP != null)
            {
                playerHP.TakeDamage(damage);
            }
        }
    }

    void PlayClashEffect(Collider2D targetCollider)
    {
        if (clashEffectPrefab == null) return;

        Vector2 contactPoint = targetCollider.ClosestPoint(transform.position);

        Instantiate(clashEffectPrefab, contactPoint, Quaternion.identity);
    }
    void PlayAndDestroy(GameObject target)
    {
        if (audioSource != null && audioSource.clip != null)
        {
            AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
        }

        Destroy(target);
    }
}

