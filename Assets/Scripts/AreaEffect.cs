using UnityEngine;
using static Codice.Client.Common.EventTracking.TrackFeatureUseEvent.Features.DesktopGUI.Filters;

public class AreaEffect : MonoBehaviour
{
    public enum EffectType { Explosion, Heal }

    public EffectType type;
    public int amount = 30;

    private PlayerHealth playerHP;

    private void Awake()
    {
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == EffectType.Explosion)
            {
                if (playerHP == null) return;
                playerHP.TakeDamage(amount);
            }
            else if (type == EffectType.Heal)
            {
                if (playerHP == null) return;
                playerHP.Heal(amount); 
            }
        }
        if (other.CompareTag("Enemy"))
        {
            if (type == EffectType.Explosion)
            {
                EnemyController enemyCtrl = other.GetComponent<EnemyController>();

                if (enemyCtrl == null) return;

                Vector2 dir = other.transform.position - transform.position;
                enemyCtrl.TakeDamage(amount, dir);
            }
        }
    }

    public void EndExplosion()
    {
        Destroy(gameObject);
    }

    public void EndHeal()
    {
        Destroy(gameObject);
    }
}
