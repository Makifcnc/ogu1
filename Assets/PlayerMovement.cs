using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 15f;              // daha güçlü zıplama
    public float glideGravityScale = 0.1f;     // süzülme sırasında gravity
    private float normalGravityScale;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool hasJumped = false;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalGravityScale = rb.gravityScale;
    }

    void Update()
    {
        // Yere temas kontrolü
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Yere basıyorsa süzülme iptal
        if (isGrounded)
        {
            hasJumped = false;
            rb.gravityScale = normalGravityScale;
        }

        // Hareket
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Zıplama
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            hasJumped = true;
        }

        // Zıpladıktan sonra havadaysa süzülme devreye girer
        if (hasJumped && !isGrounded && rb.linearVelocity.y <= 0f)
        {
            rb.gravityScale = glideGravityScale;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
