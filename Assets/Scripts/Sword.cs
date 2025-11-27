using UnityEngine;
using static Codice.Client.Common.EventTracking.TrackFeatureUseEvent.Features.DesktopGUI.Filters;

public class Sword : MonoBehaviour
{
    public int level = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Sword otherSword = other.GetComponent<Sword>();
        if (otherSword != null)
        {
            if (!gameObject.activeInHierarchy || !otherSword.gameObject.activeInHierarchy)
                return;

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
        EnemyController enemy =
            other.GetComponent<EnemyController>() ??
            other.GetComponentInParent<EnemyController>();

        if (enemy != null)
        {
           

            int damage = level * 10;   
            Debug.Log("damage take" + damage);

            enemy.TakeDamage(damage);
            return;
        }
        Debug.Log("hit something not enemy or blade" + other.name);
    }
}