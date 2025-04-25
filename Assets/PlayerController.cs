using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private bool moveForward = false;   // Ýleri hareket kontrolü
    private bool moveBackward = false;  // Geri hareket kontrolü
    private bool jumpInput = false;     // Zýplama kontrolü

    public float moveSpeed = 5f;   // Yürüme hýzý
    public float jumpForce = 10f;  // Zýplama gücü

    private Rigidbody2D rb;   // Rigidbody2D bileþeni
    private PlayerInputActions playerActions; // PlayerInputActions script

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerActions = new PlayerInputActions();  // Input Actions sýnýfýný oluþtur

        // MoveF (Ýleri hareket)
        playerActions.Player.MoveF.performed += ctx => moveForward = true;
        playerActions.Player.MoveF.canceled += ctx => moveForward = false;

        // MoveB (Geri hareket)
        playerActions.Player.MoveB.performed += ctx => moveBackward = true;
        playerActions.Player.MoveB.canceled += ctx => moveBackward = false;

        // Jump (Zýplama)
        playerActions.Player.Jump.performed += ctx => jumpInput = true;
        playerActions.Player.Jump.canceled += ctx => jumpInput = false;
    }

    private void OnEnable()
    {
        playerActions.Enable();  // Input Actions'ý aktif et
    }

    private void OnDisable()
    {
        playerActions.Disable(); // Input Actions'ý devre dýþý býrak
    }

    private void Update()
    {
        // Yürüme iþlemi (ileri ve geri)
        if (moveForward)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y); // Ýleri hareket
        }
        else if (moveBackward)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y); // Geri hareket
        }

        // Zýplama iþlemi
        if (jumpInput && Mathf.Abs(rb.velocity.y) < 0.001f) // Zýplama sadece yerdeyken yapýlabilir
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);  // Zýplama kuvveti ekle
        }
    }
}
