using UnityEngine;
using static Codice.Client.Common.EventTracking.TrackFeatureUseEvent.Features.DesktopGUI.Filters;

public class Sword : MonoBehaviour
{
    public int level = 0;

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
    }
}

