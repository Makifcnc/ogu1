using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private SpriteRenderer sprite;

    private void Awake()
    {
        // Fizik ve çizim bileşenlerini al
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Ateş topunun yönünü dışarıdan ayarlar.
    /// </summary>
    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir.normalized;


        // bu yanlış
        if (sprite != null)
            sprite.flipX = dir.x < 0f;
    }

    private void Start()
    {
        // Belirttiğin süre sonra kendini yok et
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        // Physics adımı: rb.velocity kullan
        if (rb != null)
            rb.linearVelocity = moveDirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fireball") || collision.gameObject.CompareTag("Player"))
        {
            return; // Kendisiyle çarpışmayı yok say
        }


        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // Enemy'yi yok et
        }

        Destroy(gameObject); // Kendini yok et
    }


}
