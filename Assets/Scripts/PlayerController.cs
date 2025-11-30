using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    private Vector2 movement;
    public bool isDead = false;

    void Update()
    {
        if (isDead)
        {
            animator.SetBool("IsMoving", false);
            animator.SetFloat("InputX", 0);
            animator.SetFloat("InputY", 0);
            return;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            animator.SetFloat("InputX", movement.x);
            animator.SetFloat("InputY", movement.y);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;
        transform.Translate(movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}