using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D coll;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource jumpSoundEffect;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;

    private enum MovementState { idle, running, jumping, falling }

    private void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (coll == null) coll = GetComponent<BoxCollider2D>();
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        if (anim == null) anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // WASD veya Yön Tuşları kontrolü
        dirX = Input.GetAxisRaw("Horizontal"); // A ve D tuşları ya da sol-sağ yön tuşları için

        // Zıplama kontrolü (W tuşu veya Space)
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            if (jumpSoundEffect != null)
                jumpSoundEffect.Play();
        }

        // Yatay hareket uygulama
        rb.linearVelocity = new Vector2(dirX * moveSpeed, rb.linearVelocity.y);

        // Animasyon güncelle
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.linearVelocity.y > 0.1f)
            state = MovementState.jumping;
        else if (rb.linearVelocity.y < -0.1f)
            state = MovementState.falling;

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
