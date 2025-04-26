using UnityEngine;
public class EarthAbility : MonoBehaviour, IAbility
{
    public ElementType ElementType => ElementType.Earth;
    public void Use()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider != null)
        {
            //var block = hit.collider.GetComponent<BreakableBlock>();
            //if (block != null) block.Break();
        }
    }
}
