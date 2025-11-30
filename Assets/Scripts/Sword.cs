using UnityEngine;
using static Codice.Client.Common.EventTracking.TrackFeatureUseEvent.Features.DesktopGUI.Filters;

public class Sword : MonoBehaviour
{
    public int level = 0;

    private PlayerHealth playerHP;

    public ParticleSystem clashEffectPrefab;

    private void Awake()
    {
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
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
                    Debug.Log("");
                    Destroy(otherSword.gameObject);
                }
                else if (level < otherSword.level)
                {
                    Debug.Log("");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("");
                    Destroy(otherSword.gameObject);
                    Destroy(gameObject);
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
}

