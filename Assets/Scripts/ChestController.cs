using UnityEngine;
using UnityEngine.Audio;

public class ChestController : MonoBehaviour
{
    private Animator animator;
    private bool isOpened = false;
    private AudioSource audioSource;

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpened) return;

        if (other.CompareTag("Player"))
        {
            OpenChest();
        }
    }


    void OpenChest()
    {
        isOpened = true;

        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
        if (audioSource != null)
        {
            audioSource.Play();  
        }

        Debug.Log("Chest open");
    }
}
