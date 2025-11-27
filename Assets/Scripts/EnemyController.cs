using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public float attackDistance = 0.7f;
    public float knockbackForce = 4f;

    public int maxHP = 30;
    private int currentHP;

    private Animator animator;
    private SpriteRenderer sprite;

    private bool isAttacking = false;
    private bool isHurt = false;
    private bool isDead = false;

    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHP = maxHP;

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }
        else
        {
            Debug.LogError("aucun joueur trouv?");
        }
    }

    void Update()
    {
        if (isDead) return;
        if (isHurt) return;
        if (isAttacking) return;

        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= attackDistance)
        {
            StartAttack();
            return;
        }

        MoveTowardPlayer();

        if (isAttacking || isHurt || isDead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
    }

    void MoveTowardPlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;

        animator.SetFloat("Speed", 1);

        if (dir.x > 0) sprite.flipX = false;
        else if (dir.x < 0) sprite.flipX = true;
    }

    void StartAttack()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetFloat("Speed", 0);

        isAttacking = true;
        animator.SetBool("IsAttacking", true);

        animator.SetInteger("AttackIndex", Random.Range(1, 3));
    }

    public void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("IsAttacking", false);
    }

    public void TakeDamage(int damage)
    {
       
        if (isDead) return;

        Vector2 hitDirection = gameObject.transform.position - player.position;


        currentHP -= damage;
        Debug.Log("Enemy TakeDamage: -" + damage + " | HP left = " + currentHP);


        isHurt = true;
        animator.SetBool("IsHurt", true);

        rb.linearVelocity = hitDirection.normalized * knockbackForce;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void EndHurt()
    {
        isHurt = false;
        animator.SetBool("IsHurt", false);
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;
        Debug.Log("Enemy Died!");
        animator.SetBool("IsDead", true);
        rb.linearVelocity = Vector2.zero;
    }

    public void OnDeathEnd()
    {
        Destroy(gameObject);
    }
}
