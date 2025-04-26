// PlayerMovementComponent.cs
using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask jumpableGround;

    [Header("Air Ability Settings")]
    [Tooltip("Hava elementindeki yüksek zıplama çarpanı")]
    [SerializeField] private float airJumpMultiplier = 1.4f;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    private enum MovementState { Idle, Running, Jumping, Falling }


    /// <summary>Normal zıplama</summary>
    [System.Obsolete]
    public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        UpdateAnimation(0);
    }

    /// <summary>Hava elementi aktifken kullanılacak daha güçlü zıplama</summary>
    [System.Obsolete]
    public void JumpHigh()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * airJumpMultiplier);
        UpdateAnimation(0);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    /// <summary>Yatay hareket ve animasyon.</summary>
    [System.Obsolete]
    public void Move(float dirX)
    {
        rb.linearVelocity = new Vector2(dirX * moveSpeed, rb.linearVelocity.y);
        UpdateAnimation(dirX);
    }


    /// <summary>Zıplama: multiplier=1 normal, multiplier=airJumpMultiplier ile yüksek zıplama.</summary>
    [System.Obsolete]
    public void Jump(float multiplier = 1f)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * multiplier);
        UpdateAnimation(0);
    }

    /// <summary>Yerden kontrolü.</summary>
    public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center,
                                 coll.bounds.size,
                                 0f,
                                 Vector2.down,
                                 0.1f,
                                 jumpableGround);
    }
    [System.Obsolete]
    private void UpdateAnimation(float dirX)
    {
        MovementState state = MovementState.Idle;
        if (rb.linearVelocity.y > 0.1f) state = MovementState.Jumping;
        else if (rb.linearVelocity.y < -0.1f) state = MovementState.Falling;
        else if (Mathf.Abs(dirX) > 0f) state = MovementState.Running;
        anim.SetInteger("state", (int)state);
    }

    /// <summary>Hava elementi için dışarıdan erişim.</summary>
    public float AirJumpMultiplier => airJumpMultiplier;
}