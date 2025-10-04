using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Assign your ghost sprite here
    public float fadeSpeed = 1f; // How fast the ghost fades

    private bool isFading = false;

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartFade()
    {
        if (!isFading)
            StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        isFading = true;
        Color originalColor = spriteRenderer.color;
        float alpha = originalColor.a;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Max(alpha, 0f));
            yield return null;
        }

        spriteRenderer.enabled = false;
        isFading = false;

        //THIS NEEDS TO STOP GHOST ANIMATION FIRST
        // THEN RESET GHOST POSITION

        // TELEPORT GHOST OUT OF FRAME
        // transform.position = new Vector3(-16, -4, 0);
        // spriteRenderer.enabled = true;
        // spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
