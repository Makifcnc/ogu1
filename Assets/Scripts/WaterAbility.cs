using UnityEngine;
public class WaterAbility : MonoBehaviour, IAbility
{
    [SerializeField] private GameObject icePrefab;
    [SerializeField] private Transform attackPoint;

    public ElementType ElementType => ElementType.Water;
    public void Use()
    {
        // Tıkladığın yere buz bloğu koy
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(icePrefab, mousePos, Quaternion.identity);
    }
}
