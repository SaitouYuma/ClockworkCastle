using UnityEngine;
using System.Collections;

public class BackgroundChanger : MonoBehaviour
{
    [Header("”wŒiÝ’è")]
    [SerializeField] private SpriteRenderer[] backgroundLayers;
    [SerializeField] private int targetBackgroundIndex = 0;
    [SerializeField] private float fadeDuration = 1f;

    private static BackgroundManager manager;

    void Awake()
    {
        // ‰‰ñ‚Ì‚Ýƒ}ƒl[ƒWƒƒ[‚ð‰Šú‰»
        if (manager == null)
        {
            manager = new BackgroundManager();
        }
    }

    void Start()
    {
        // ”wŒiƒŒƒCƒ„[‚ÌŽQÆ‚ð“o˜^
        if (backgroundLayers != null && backgroundLayers.Length > 0)
        {
            manager.RegisterLayers(backgroundLayers);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !manager.IsChanging
            && manager.CurrentIndex != targetBackgroundIndex)
        {
            StartCoroutine(manager.FadeToBackground(targetBackgroundIndex, fadeDuration));
        }
    }
}

// ”wŒiŠÇ——pƒNƒ‰ƒX
public class BackgroundManager
{
    private SpriteRenderer[] layers;
    private int currentIndex = 0;
    private bool isChanging = false;

    public int CurrentIndex => currentIndex;
    public bool IsChanging => isChanging;

    public void RegisterLayers(SpriteRenderer[] backgroundLayers)
    {
        if (layers == null)
        {
            layers = backgroundLayers;
            InitializeLayers();
        }
    }

    private void InitializeLayers()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            if (layers[i] != null)
            {
                Color color = layers[i].color;
                layers[i].color = new Color(color.r, color.g, color.b, i == 0 ? 1f : 0f);
            }
        }
    }

    public IEnumerator FadeToBackground(int newIndex, float duration)
    {
        if (layers == null || newIndex >= layers.Length || newIndex < 0)
            yield break;

        isChanging = true;

        SpriteRenderer oldBg = layers[currentIndex];
        SpriteRenderer newBg = layers[newIndex];

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            if (oldBg != null)
            {
                Color c = oldBg.color;
                oldBg.color = new Color(c.r, c.g, c.b, 1f - t);
            }

            if (newBg != null)
            {
                Color c = newBg.color;
                newBg.color = new Color(c.r, c.g, c.b, t);
            }

            yield return null;
        }

        // ÅI’l‚ðŠmŽÀ‚ÉÝ’è
        if (oldBg != null)
        {
            Color c = oldBg.color;
            oldBg.color = new Color(c.r, c.g, c.b, 0f);
        }

        if (newBg != null)
        {
            Color c = newBg.color;
            newBg.color = new Color(c.r, c.g, c.b, 1f);
        }

        currentIndex = newIndex;
        isChanging = false;
    }
}