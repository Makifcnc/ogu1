using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueSO dialogueSO;


    public void SendDialogMessage()
    {
        DialogueManager.Instance.StartDialogue(dialogueSO);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player trigger alanına girdi!");
            DialogueManager.Instance.StartDialogue(dialogueSO);
            Destroy(gameObject);
        }
    }

}