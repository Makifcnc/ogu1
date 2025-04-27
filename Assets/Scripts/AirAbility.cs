using UnityEngine;

public class AirAbility : MonoBehaviour, IAbility
{
    private PlayerMovementComponent mover;

    private void Awake()
    {
        // artık PlayerMovementComponent referansı
        mover = GetComponent<PlayerMovementComponent>();
    }

    public ElementType ElementType => ElementType.Air;

    /// <summary>
    /// Hava elementindeyken, farenin sol tıkıyla
    /// ekstra güçlü zıplama yapar.
    /// </summary>
    /// 
    [System.Obsolete]
    public void Use()
    {
        // Yerdeyken normalden daha güçlü bir zıplama
        if (mover.IsGrounded())
        {
            mover.JumpHigh();
            AudioManager.Instance.PlaySFX("Jump");
            Debug.Log("[AirAbility] Yüksek zıplama!");
        }
    }
}
