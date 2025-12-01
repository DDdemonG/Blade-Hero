using UnityEngine;

public class BuffItem : MonoBehaviour
{
    public enum BuffType
    {
        SpeedUp,
        Shield,
        SpinSpeed
    }

    public BuffType type;
    public float amount;
    public float duration;
    private AudioSource audioSource;
    public AudioClip sound;

    public float pickupDelay = 0.5f;
    private float spawnTime;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        spawnTime = Time.time;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.time < spawnTime + pickupDelay) return;

        if (other.CompareTag("Player"))
        {
            PlayerBuffs player = other.GetComponent<PlayerBuffs>();

            if (player != null)
            {
                player.ApplyBuff(type, amount, duration);
                PlayAndDestroy(gameObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time < spawnTime + pickupDelay) return;
        OnTriggerEnter2D(other);
    }

    void PlayAndDestroy(GameObject effect)
    {
        if (audioSource != null && sound != null)
        {
            audioSource.PlayOneShot(sound, 0.6f);

            foreach (var r in GetComponentsInChildren<Renderer>())
            {
                r.enabled = false;
            }
            GetComponent<Collider2D>().enabled = false;
            

            Destroy(effect, sound.length);
        }
        else
        {
            Destroy(effect);
        }
    }
}
