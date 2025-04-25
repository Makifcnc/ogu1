using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    public void SetDirection(Vector2 dir)
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = dir.normalized;
    }

    private void Start()
    {

        Destroy(gameObject, lifeTime); // otomatik silinsin
    }

    private void Update()
    {
        rb.linearVelocity = moveDirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // düşmanı yok et
            Destroy(gameObject);           // ateşi de yok et
        }
        // else if (collision.gameObject.CompareTag("Obstacle"))
        // {
        //     Destroy(gameObject); // diğer şeylere çarpınca da kaybol
        // }
    }
}
