using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private Animator animator;
    private bool isOpening = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerInventory.Instance.HasKey && !isOpening)
        {
            isOpening = true;
            animator.SetTrigger("Open"); // Open trigger çalışır
            AudioManager.Instance.PlaySFX("Door"); // Kapı açılma sesi
            Invoke(nameof(LoadNextLevel), 3f); // 1.5 saniye sonra sahne geçsin (animasyon süresi kadar)
        }
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
