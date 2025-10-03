using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    [Header("Fade Settings")]
    [SerializeField] private Image fadeImage;             // assign your FadeImage
    [SerializeField] private float fadeOutDuration = 1f;  // seconds for fade out
    [SerializeField] private float fadeInDuration = 1f;   // seconds for fade in
    [SerializeField] private bool startFadedIn = true;    // start visible or transparent

    private bool isTransitioning = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (fadeImage == null)
        {
            Debug.LogError("SceneTransitionManager: FadeImage not assigned.");
            return;
        }

        // Set initial alpha
        Color c = fadeImage.color;
        c.a = startFadedIn ? 0f : 1f;
        fadeImage.color = c;

        if (startFadedIn)
            StartCoroutine(FadeInCoroutine());
    }

    /// <summary>
    /// Fade to a scene by name
    /// </summary>
    public void FadeToScene(string sceneName)
    {
        if (!isTransitioning)
            StartCoroutine(TransitionToSceneCoroutine(sceneName));
    }

    private IEnumerator TransitionToSceneCoroutine(string sceneName)
    {
        yield return StartCoroutine(FadeOutCoroutine());
        yield return SceneManager.LoadSceneAsync(sceneName);
        yield return StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        isTransitioning = true;
        fadeImage.raycastTarget = true; // block input
        yield return StartCoroutine(Fade(0f, 1f, fadeOutDuration));
    }

    private IEnumerator FadeInCoroutine()
    {
        yield return StartCoroutine(Fade(1f, 0f, fadeInDuration));
        fadeImage.raycastTarget = false;
        isTransitioning = false;
    }

    /// <summary>
    /// Generic fade coroutine
    /// </summary>
    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color c = fadeImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            c.a = alpha;
            fadeImage.color = c;
            yield return null;
        }

        // Ensure final alpha is exact
        c.a = endAlpha;
        fadeImage.color = c;
    }
}
