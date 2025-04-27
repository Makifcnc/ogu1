using UnityEngine;

public class WaterAbility : MonoBehaviour, IAbility
{
    [Header("References")]
    [SerializeField] private GameObject icePrefab;
    [SerializeField] private Transform attackPoint;

    [Header("Animation")]
    [SerializeField] private Animator anim;
    [SerializeField] private string iceTrigger = "isIceing";

    public ElementType ElementType => ElementType.Water;

    // 1) Bu metot sol tık anında çağrılır, sadece animasyonu tetikler.
    public void Use()
    {
        if (anim == null)
        {
            Debug.LogError("[WaterAbility] Animator bulunamadı!");
            return;
        }
        anim.ResetTrigger(iceTrigger);
        anim.SetTrigger(iceTrigger);
    }

    // 2) “isIceing” animasyonunun son karesinde Animation Event olarak çağrılacak.
    //    Burada gerçekten buz prefab’ını instantiate ediyoruz.
    public void SpawnIce()
    {
        if (icePrefab == null || attackPoint == null)
        {
            Debug.LogWarning("[WaterAbility] icePrefab veya attackPoint eksik!");
            return;
        }

        // Buzu yarat
        GameObject ice = Instantiate(
            icePrefab,
            attackPoint.position,
            attackPoint.rotation
        );
        AudioManager.Instance.PlaySFX("Ice");


        // 5 saniyede fade-out efekti
        var fade = ice.AddComponent<IceFadeOut>();
        fade.StartFade(5f);
    }
}
