using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using System;

public class BackgroundChanger : MonoBehaviour
{
    [Header("背景設定")]
    [SerializeField] SpriteRenderer[] backgroundLayers; // 全ての背景（4つ）
    [SerializeField] int targetBackgroundIndex = 0; // このエリアで表示する背景（0?3）
    [SerializeField] float fadeDuration = 1f;

    private static int currentBackgroundIndex = 0; // 現在表示中の背景
    private static bool isChanging = false;

    void Start()
    {
        // 初期状態：最初の背景だけ表示
        if (backgroundLayers != null && backgroundLayers.Length > 0)
        {
            for (int i = 0; i < backgroundLayers.Length; i++)
            {
                if (backgroundLayers[i] != null)
                {
                    Color color = backgroundLayers[i].color;
                    backgroundLayers[i].color = new Color(color.r, color.g, color.b, i == 0 ? 1f : 0f);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isChanging && currentBackgroundIndex != targetBackgroundIndex)
        {
            StartCoroutine(FadeToBackground(targetBackgroundIndex));
        }
    }

    IEnumerator FadeToBackground(int newIndex)
    {
        if (backgroundLayers == null || newIndex >= backgroundLayers.Length || newIndex < 0)
            yield break;

        isChanging = true;

        SpriteRenderer oldBackground = backgroundLayers[currentBackgroundIndex];
        SpriteRenderer newBackground = backgroundLayers[newIndex];

        float elapsed = 0f;

        // クロスフェード
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            // 古い背景をフェードアウト
            if (oldBackground != null)
            {
                Color oldColor = oldBackground.color;
                oldBackground.color = new Color(oldColor.r, oldColor.g, oldColor.b, 1f - t);
            }

            // 新しい背景をフェードイン
            if (newBackground != null)
            {
                Color newColor = newBackground.color;
                newBackground.color = new Color(newColor.r, newColor.g, newColor.b, t);
            }

            yield return null;
        }

        // 最終的な透明度を設定
        if (oldBackground != null)
        {
            Color oldColor = oldBackground.color;
            oldBackground.color = new Color(oldColor.r, oldColor.g, oldColor.b, 0f);
        }

        if (newBackground != null)
        {
            Color newColor = newBackground.color;
            newBackground.color = new Color(newColor.r, newColor.g, newColor.b, 1f);
        }

        currentBackgroundIndex = newIndex;
        isChanging = false;
    }
}
