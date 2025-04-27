using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    private bool isDead = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Çarptığın obje: {collision.gameObject.name} - Tag: {collision.gameObject.tag}");

        if (isDead) return;

        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Öldün! Düşman veya Tuzağa çarptın.");
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return; // Zaten öldüyse tekrar işlem yapma
        isDead = true;
        Debug.Log("Oyuncu öldü, sahne yeniden yükleniyor...");
        Invoke(nameof(RestartLevel), 0.5f);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}