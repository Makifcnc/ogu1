using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX("Collect");
            PlayerInventory.Instance.HasKey = true;
            Destroy(gameObject); // Anahtar yok oluyor
        }
    }
}
