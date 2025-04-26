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

    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform attackpoint;
    private float originalAttackPointX;


    private float dirX = 0f;

    private enum MovementState { idle, running, jumping, falling }

    private void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (coll == null) coll = GetComponent<BoxCollider2D>();
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        if (anim == null) anim = GetComponent<Animator>();
        originalAttackPointX = attackpoint.localPosition.x;

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

        // Ateş topu atma kontrolü (F tuşu)
        rb.linearVelocity = new Vector2(dirX * moveSpeed, rb.linearVelocity.y);
        UpdateAnimationState();

        if (Input.GetMouseButtonDown(0)) // sol click
        {
            anim.ResetTrigger("isShooting"); // varsa sıfırla
            anim.SetTrigger("isShooting");   // sonra tekrar tetikle
            Fire();
            Debug.Log("Ateş ettim!");
        }

        // Sprite yönüne göre attackpoint pozisyonunu ayarla
        if (sprite.flipX)
        {
            attackpoint.localPosition = new Vector3(-Mathf.Abs(originalAttackPointX), attackpoint.localPosition.y, attackpoint.localPosition.z);
        }
        else
        {
            attackpoint.localPosition = new Vector3(Mathf.Abs(originalAttackPointX), attackpoint.localPosition.y, attackpoint.localPosition.z);
        }


        //
    }

    private void Fire()
    {
        GameObject fireball = Instantiate(fireballPrefab, attackpoint.position, Quaternion.identity);

        Vector2 fireDirection = sprite.flipX ? Vector2.left : Vector2.right;

        fireball.GetComponent<Fireball>().SetDirection(fireDirection);
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

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
