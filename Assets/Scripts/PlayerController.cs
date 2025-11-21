using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 input;
    private Vector2 lastDir = Vector2.down;

    private string currentAnimation = "";

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input = input.normalized;

        if (input.sqrMagnitude > 0.01f)
            lastDir = input;

        PlayCorrectAnimation();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * moveSpeed * Time.fixedDeltaTime);
    }

    void PlayCorrectAnimation()
    {
        string newAnim = "";

        // 判断方向
        if (Mathf.Abs(lastDir.x) > Mathf.Abs(lastDir.y))
        {
            if (lastDir.x > 0) newAnim = "Idle_right";
            else newAnim = "Idle_left";
        }
        else
        {
            if (lastDir.y > 0) newAnim = "Idle_up";
            else newAnim = "Idle_down";
        }

        // 如果动画没变 → 不要播放（彻底防止闪烁）
        if (currentAnimation == newAnim)
        {
            // 如果动画没播完，直接跳过，不打断
            AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
            if (info.IsName(newAnim) && info.normalizedTime < 1f)
                return;
        }

        // 真的变化才播放一次
        anim.Play(newAnim, 0, 0f);
        currentAnimation = newAnim;
    }

}