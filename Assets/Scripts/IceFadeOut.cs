using UnityEngine;

public class IceFadeOut : MonoBehaviour
{
    private SpriteRenderer sr;
    private float lifetime;
    private float fadeDuration = 1f; // Son 1 saniyede şeffaflaşsın

    private float timer;

    public void StartFade(float totalLifetime)
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogWarning("[IceFadeOut] SpriteRenderer bulunamadı!");
            return;
        }

        lifetime = totalLifetime;
        timer = 0f;
    }

    private void Update()
    {
        if (sr == null) return;

        timer += Time.deltaTime;

        if (timer >= lifetime - fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, (timer - (lifetime - fadeDuration)) / fadeDuration);
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }

        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
