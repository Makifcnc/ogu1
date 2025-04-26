using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    private bool isDead = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return; // Eğer zaten öldüyse, ikinci kez tetiklenmesin

        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("oldun");
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
