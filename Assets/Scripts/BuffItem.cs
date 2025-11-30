using UnityEngine;

public class BuffItem : MonoBehaviour
{
    public enum BuffType
    {
        SpeedUp,
        AttackUp,
        Shield,
        SpinSpeed
    }

    public BuffType type;
    public float amount;
    public float duration;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBuffs player = other.GetComponent<PlayerBuffs>();

            if (player != null)
            {
                player.ApplyBuff(type, amount, duration);
                Destroy(gameObject);
            }
        }
    }
}
