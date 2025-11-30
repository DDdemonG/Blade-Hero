using UnityEngine;

public class Sword : MonoBehaviour
{
    public int level = 0;

    private AudioSource audioSource;

    private PlayerHealth playerHP;

    public ParticleSystem clashEffectPrefab;

    private void Awake()
    {
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("PlayerBlade")) { 
            Sword otherSword = other.GetComponent<Sword>();
            if (otherSword != null)
            {
                if (!gameObject.activeInHierarchy || !otherSword.gameObject.activeInHierarchy)
                    return;

                if (other.CompareTag("PlayerBlade"))
                {
                    return;
                }

                PlayClashEffect(other);
                if (level > otherSword.level)
                {
                    PlayClashEffect(other);
                    PlayAndDestroy(otherSword.gameObject);
                }
                else if (level < otherSword.level)
                {
                    PlayClashEffect(other);
                    PlayAndDestroy(gameObject);
                }
                else
                {
                    PlayClashEffect(other);
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
                int damage = level * 10;

                enemy.TakeDamage(damage);
                return;
            }
        }

        if (gameObject.CompareTag("EnemyBlade") && other.gameObject.CompareTag("Player"))
        {
            if (playerHP != null)
            {
                playerHP.TakeDamage(level*10);
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

