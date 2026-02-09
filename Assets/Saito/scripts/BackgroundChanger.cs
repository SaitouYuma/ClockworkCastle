using UnityEngine;
using System.Collections;

public class BackgroundChanger : MonoBehaviour
{
    [Header("背景設定")]
    [SerializeField] private SpriteRenderer[] backgroundLayers;
    [SerializeField] private int targetBackgroundIndex = 0;
    [SerializeField] private float fadeDuration = 1f;

    private static BackgroundManager manager;

    void Awake()
    {
        // シーン読み込み時にマネージャーをリセット
        if (manager == null || manager.NeedsReset())
        {
            manager = new BackgroundManager();
        }
    }

    void Start()
    {
        // 背景レイヤーの参照を登録
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

// 背景管理用クラス
public class BackgroundManager
{
    private SpriteRenderer[] layers;
    private int currentIndex = 0;
    private bool isChanging = false;

    public int CurrentIndex => currentIndex;
    public bool IsChanging => isChanging;

    // レイヤーが破棄されているかチェック
    public bool NeedsReset()
    {
        return layers != null && layers.Length > 0 && layers[0] == null;
    }

    public void RegisterLayers(SpriteRenderer[] backgroundLayers)
    {
        // 既に登録済みかつ有効ならスキップ
        if (layers != null && layers.Length > 0 && layers[0] != null)
        {
            return;
        }

        layers = backgroundLayers;
        InitializeLayers();
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

        // 最終値を確実に設定
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