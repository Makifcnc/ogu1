using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 15f, -15f);

    [SerializeField] private float followSpeed = 3f;  // ne kadar büyükse daha hızlı “takip”

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = target.position + offset;
        // Lerp: mevcut pozisyon ile hedef pozisyon arasında interpolate eder
        transform.position = Vector3.Lerp(
            transform.position,
            desired,
            followSpeed * Time.deltaTime
        );
    }
}
