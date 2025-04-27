using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private DialogueSO dialogueSO;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private short facingRight = 1;
    [SerializeField] private DialogueSO actionableDialog;

    private Animator anim;
    private bool isAnimating = false;
    private bool isPlayerNear = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        Debug.Log($"Başlangıçta atanmış DialogueSO: {dialogueSO.name}");
    }


    void OnEnable()
    {
        DialogueManager.DialogueEnterEvent += DialogueEnter;
        DialogueManager.DialogueExitEvent += DialogueExit;
    }

    void OnDisable()
    {
        DialogueManager.DialogueEnterEvent -= DialogueEnter;
        DialogueManager.DialogueExitEvent -= DialogueExit;
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void DialogueEnter(DialogueSO asd)
    {
        if (isAnimating || actionableDialog == null) return;

        if (actionableDialog == asd)
        {
            isAnimating = true;
            //AudioManager.Instance.PlayMusic("music1");
        }
    }

    private void DialogueExit(DialogueSO asd)
    {
        // Dialog bitince yapılacaklar (şimdilik boş bırakabiliriz)
    }

    public void Interact()
    {
        DialogueManager.Instance.StartDialogue(dialogueSO);
        infoPanel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FaceControl(collision.transform.position);
            infoPanel.SetActive(true);
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            infoPanel.SetActive(false);
            isPlayerNear = false;
        }
    }

    private void FaceControl(Vector2 targetPos)
    {
        if (facingRight == 1 && targetPos.x < transform.position.x || facingRight == -1 && targetPos.x > transform.position.x)
        {
            facingRight *= -1;
            transform.rotation = Quaternion.Euler(0, facingRight == 1 ? 0 : 180, 0);
            infoPanel.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
