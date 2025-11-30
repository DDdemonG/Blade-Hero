using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator animator;
    private bool isOpened = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
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

        Debug.Log("Chest open");
    }
}
