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

    public int attackDamage = 10;

    private PlayerHealth playerHP;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerHP = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
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
    }

    void MoveTowardPlayer()
    {
        if (isHurt || isDead || isDead) return;

        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;

        animator.SetFloat("Speed", 1);

        if (dir.x > 0) sprite.flipX = false;
        else if (dir.x < 0) sprite.flipX = true;
    }

    void StartAttack()
    {
        if (!player.gameObject.activeInHierarchy) return;

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
        if (isDead || isHurt) return;

        currentHP -= damage;
        Debug.Log("Enemy TakeDamage: -" + damage + " | HP left = " + currentHP);

        if (currentHP <= 0)
        {
            Die();
            KnockBack();
        }
        else
        {
            isHurt = true;
            animator.SetBool("IsHurt", true);
            KnockBack();
        }
    }

    public void KnockBack()
    {
        Vector2 hitDirection = gameObject.transform.position - player.position;
        rb.linearVelocity = hitDirection.normalized * knockbackForce;
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

    public void MeleeAttackHit()
    {
        if (!player.gameObject.activeInHierarchy || isDead) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist > attackDistance + 0.5f) return;

        Vector2 dirToPlayer = (player.position - transform.position).normalized;

        bool isFacingLeft = sprite.flipX;

        if (isFacingLeft && dirToPlayer.x > 0) return;

        if (!isFacingLeft && dirToPlayer.x < 0) return;

        Debug.Log("Hit Player!");
        
        if (playerHP != null)
        {
            playerHP.TakeDamage(attackDamage);
        }
        else
        {
            Debug.LogWarning("aucun PlayerHealth");
        }
        
    }
}
