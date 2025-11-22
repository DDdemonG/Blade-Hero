using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public int swordLevel = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!gameObject.CompareTag("LootBlade")) return;

            SwordManager manager = other.GetComponent<SwordManager>();

            if (manager != null)
            {
                manager.AddSword(swordLevel);

                Destroy(gameObject);
            }
        }
    }
}
