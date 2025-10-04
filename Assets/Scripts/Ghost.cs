using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour {
    [SerializeField] private ShadowReplay ghostReplay;
    [SerializeField] private Animator animator;
    public SpriteRenderer spriteRenderer; // Assign your ghost sprite here
    public float fadeSpeed = 1f; // How fast the ghost fades

    private bool isFading = false;

    public Transform startTransform;
    public Vector2 inititalPosition;

    void Awake() {
        inititalPosition = startTransform.position;
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartFade() {
        if (!isFading)
            StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine() {
        isFading = true;
        Color originalColor = spriteRenderer.color;
        float alpha = originalColor.a;

        while (alpha > 0f) {
            alpha -= Time.deltaTime * fadeSpeed;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Max(alpha, 0f));
            yield return null;
        }

        spriteRenderer.enabled = false;
        isFading = false;

        ghostReplay.beginReplay = false;

        transform.position = inititalPosition;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
    }
}
