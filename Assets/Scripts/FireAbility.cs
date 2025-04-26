using UnityEngine;
public class FireAbility : MonoBehaviour, IAbility
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform attackPoint;
    private Animator anim;
    private SpriteRenderer sprite;
    private float originalAttackPointX;
    [Header("Animation")]
    [SerializeField] private string shootTrigger = "isShooting";



    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        // AttackPoint'un Inspector'daki local X'ini saklıyoruz
        anim = GetComponent<Animator>();
        originalAttackPointX = attackPoint.localPosition.x;
    }

    public ElementType ElementType => ElementType.Fire;
    public void Use()
    {
        anim.ResetTrigger(shootTrigger);
        anim.SetTrigger(shootTrigger);

        // 1) AttackPoint'u çevir (sol-sağ bakışa göre)
        float x = sprite.flipX
            ? -Mathf.Abs(originalAttackPointX)
            : Mathf.Abs(originalAttackPointX);
        attackPoint.localPosition = new Vector3(
            x,
            attackPoint.localPosition.y,
            attackPoint.localPosition.z
        );

        // İstersen point'un kendisini de 180° döndürebilirsin:
        attackPoint.localRotation = sprite.flipX
            ? Quaternion.Euler(0, 0, 180)
            : Quaternion.identity;

        // 2) Ateş topunu instantiate et
        GameObject fb = Instantiate(
            fireballPrefab,
            attackPoint.position,
            Quaternion.identity
        );

        // 3) Gideceği yönü ayarla
        Vector2 dir = sprite.flipX ? Vector2.left : Vector2.right;
        fb.GetComponent<Fireball>().SetDirection(dir);
    }
}
