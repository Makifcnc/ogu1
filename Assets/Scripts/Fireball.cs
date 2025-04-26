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

        // Sprite'ı da buna göre sağ/sol çevir
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
        // Düşmanla çarpışınca ikisini de yok et
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            return;
        }
        // Eğer istemiyorsan başka nesneler ateşi durdurmasın,
        // Player ile de çarpışmayı önlemek için layer ayarları yapmalısın.
    }
}
