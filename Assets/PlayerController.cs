using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private bool moveForward = false;   // �leri hareket kontrol�
    private bool moveBackward = false;  // Geri hareket kontrol�
    private bool jumpInput = false;     // Z�plama kontrol�

    public float moveSpeed = 5f;   // Y�r�me h�z�
    public float jumpForce = 10f;  // Z�plama g�c�

    private Rigidbody2D rb;   // Rigidbody2D bile�eni
    private PlayerInputActions playerActions; // PlayerInputActions script

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerActions = new PlayerInputActions();  // Input Actions s�n�f�n� olu�tur

        // MoveF (�leri hareket)
        playerActions.Player.MoveF.performed += ctx => moveForward = true;
        playerActions.Player.MoveF.canceled += ctx => moveForward = false;

        // MoveB (Geri hareket)
        playerActions.Player.MoveB.performed += ctx => moveBackward = true;
        playerActions.Player.MoveB.canceled += ctx => moveBackward = false;

        // Jump (Z�plama)
        playerActions.Player.Jump.performed += ctx => jumpInput = true;
        playerActions.Player.Jump.canceled += ctx => jumpInput = false;
    }

    private void OnEnable()
    {
        playerActions.Enable();  // Input Actions'� aktif et
    }

    private void OnDisable()
    {
        playerActions.Disable(); // Input Actions'� devre d��� b�rak
    }

    private void Update()
    {
        // Y�r�me i�lemi (ileri ve geri)
        if (moveForward)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y); // �leri hareket
        }
        else if (moveBackward)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y); // Geri hareket
        }

        // Z�plama i�lemi
        if (jumpInput && Mathf.Abs(rb.velocity.y) < 0.001f) // Z�plama sadece yerdeyken yap�labilir
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);  // Z�plama kuvveti ekle
        }
    }
}
