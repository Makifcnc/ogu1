using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    private bool isDead = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return; // Eğer zaten öldüyse, ikinci kez tetiklenmesin

        if (collision.CompareTag("Trap") || collision.CompareTag("Enemy"))
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        // İstersen burada "ölme" animasyonu veya ses efekti çalabilirsin
        Invoke(nameof(RestartLevel), 1f); // 1 saniye gecikmeli restart (animasyon için zaman)
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
