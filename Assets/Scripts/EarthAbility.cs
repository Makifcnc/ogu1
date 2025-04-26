using UnityEngine;
using UnityEngine.Tilemaps;

public class EarthAbility : MonoBehaviour, IAbility
{
    public ElementType ElementType => ElementType.Earth;

    [Tooltip("Altındaki kırılabilir blokları içeren Tilemap")]
    [SerializeField] private Tilemap breakableTilemap;

    // Ne kadar aşağıya bakacağımızı kontrol eden offset
    [SerializeField] private float checkOffsetY = 0.1f;

    [Header("Animation")]
    [Tooltip("Animator’daki punch trigger adı")]
    [SerializeField] private string punchTrigger = "isPunching";

    private BoxCollider2D coll;
    private Animator anim;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        if (breakableTilemap == null)
            Debug.LogError("[EarthAbility] breakableTilemap atanmamış!");
    }

    public void Use()
    {
        Debug.Log("[EarthAbility] Use() tetiklendi!");
        if (anim != null)
        {
            anim.ResetTrigger(punchTrigger);
            anim.SetTrigger(punchTrigger);
        }

        if (breakableTilemap == null) return;

        // 1) Dünyadaki "ayağımızın altı" noktasını bul
        Bounds b = coll.bounds;
        Vector3 worldPoint = new Vector3(
            b.center.x,
            b.min.y - checkOffsetY,
            b.center.z
        );

        // 2) Tilemap hücresine dönüştür
        Vector3Int cellPos = breakableTilemap.WorldToCell(worldPoint);

        // 3) Hücrede gerçekten kırılabilir bir tile var mı?
        TileBase tile = breakableTilemap.GetTile(cellPos);
        if (tile != null)
        {
            // 4) Kır: tile’ı kaldır
            breakableTilemap.SetTile(cellPos, null);
            // İstersen parçacık, ses, puan vb. ekle burada
            Debug.Log($"[EarthAbility] Kırıldı: {cellPos}");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (coll != null && breakableTilemap != null)
        {
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

}
