using UnityEngine;
using UnityEngine.Tilemaps;

public class EarthAbility : MonoBehaviour, IAbility
{
    public ElementType ElementType => ElementType.Earth;

    [Tooltip("Altındaki kırılabilir blokları içeren Tilemap")]
    [SerializeField] private Tilemap breakableTilemap;
    [Tooltip("Ayağın altını ne kadar aşağı kontrol edelim")]
    [SerializeField] private float checkOffsetY = 0.1f;

    [Header("Animation")]
    [SerializeField] private string punchTrigger = "isPunching";

    private BoxCollider2D coll;
    private Animator anim;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        if (breakableTilemap == null)
            Debug.LogError("[EarthAbility] breakableTilemap atanmamış!");
        if (anim == null)
            Debug.LogError("[EarthAbility] Animator bulunamadı!");
    }

    // 1) Sol tıkta sadece animasyonu tetikleyecek
    public void Use()
    {
        anim.ResetTrigger(punchTrigger);
        anim.SetTrigger(punchTrigger);
    }

    // 2) Animasyonun son karesine Animation Event olarak ekleyeceğin metod
    public void SpawnBreak()
    {
        if (breakableTilemap == null || coll == null) return;

        Bounds b = coll.bounds;
        Vector3 worldPoint = new Vector3(
            b.center.x,
            b.min.y - checkOffsetY,
            b.center.z
        );
        Vector3Int cellPos = breakableTilemap.WorldToCell(worldPoint);

        TileBase tile = breakableTilemap.GetTile(cellPos);
        if (tile != null)
        {
            breakableTilemap.SetTile(cellPos, null);
            Debug.Log($"[EarthAbility] Kırıldı: {cellPos}");
        }
        else
        {
            Debug.Log($"[EarthAbility] Kırılacak tile yok: {cellPos}");
        }
    }

    // (İsteğe bağlı) sahnede kontrol noktası göstermek için
    private void OnDrawGizmosSelected()
    {
        if (coll == null || breakableTilemap == null) return;

        Bounds b = coll.bounds;
        Vector3 worldPoint = new Vector3(
            b.center.x,
            b.min.y - checkOffsetY,
            b.center.z
        );
        Vector3Int cellPos = breakableTilemap.WorldToCell(worldPoint);
        Vector3 cellCenter = breakableTilemap.GetCellCenterWorld(cellPos);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(cellCenter, 0.1f);
    }
}
