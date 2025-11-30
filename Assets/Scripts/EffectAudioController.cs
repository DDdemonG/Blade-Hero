using UnityEngine;

public class EffectAudioController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip sound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        PlayAndDestroy();
    }

    void PlayAndDestroy()
    {
        if (sound != null)
        {
            audioSource.PlayOneShot(sound, 0.7f);
        }

        Destroy(gameObject, sound.length);
    }
}
