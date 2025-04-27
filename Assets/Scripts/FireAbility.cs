using UnityEngine;

public class FireAbility : MonoBehaviour, IAbility
{
    [Header("Fireball Settings")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float offsetForward = 0.5f;
    [SerializeField] private float offsetUp = 0.2f;

    [Header("Animation")]
    [SerializeField] private string shootTrigger = "isShooting";

    private Animator anim;

    public ElementType ElementType => ElementType.Fire;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
            Debug.LogError($"[FireAbility] Animator bulunamadı: {gameObject.name}");
    }

    // 1) Sol tıkta sadece animasyonu tetikleyecek
    public void Use()
    {
        if (anim != null)
        {
            anim.ResetTrigger(shootTrigger);
            anim.SetTrigger(shootTrigger);
        }
    }

    // 2) Animasyonun son karesine ekleyeceğin Animation Event bunu çağıracak
    public void SpawnFireball()
    {
        // Hangi yöne bakıyoruz?
        float dirX = transform.localScale.x >= 0f ? 1f : -1f;
        Vector3 dir = new Vector3(dirX, 0f, 0f);

        // Doğurulacak pozisyon
        Vector3 spawnPos = transform.position
                         + dir * offsetForward
                         + Vector3.up * offsetUp;

        // Fireball instantiate
        var fb = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);
        AudioManager.Instance.PlaySFX("Fire");


        // Yön bilgisini gönder
        if (fb.TryGetComponent<Fireball>(out var fireball))
            fireball.SetDirection(new Vector2(dirX, 0f));

        // Sprite flip
        if (fb.TryGetComponent<SpriteRenderer>(out var sr))
            sr.flipX = dirX < 0f;
    }

}
