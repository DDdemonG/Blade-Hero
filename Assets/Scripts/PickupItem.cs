using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public int swordLevel = 0;
    public float pickupDelay = 0.5f;
    private float spawnTime;

    void Start()
    {
        spawnTime = Time.time;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.time < spawnTime + pickupDelay) return;

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
