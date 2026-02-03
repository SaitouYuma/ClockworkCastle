using UnityEngine;

public class PivotPathVisualizer : MonoBehaviour
{
    [Header("経路表示設定")]
    [SerializeField] Vector3 startPosition; // 開始位置（ワールド座標）
    [SerializeField] Vector3 endPosition;   // 終了位置（ワールド座標）
    [SerializeField] Color pathColor = Color.gray;
    [SerializeField] float lineWidth = 0.1f;
    [SerializeField] int sortingOrder = -100;

    [Header("スプライト設定")]
    [SerializeField] Sprite pipeSprite;

    private GameObject pipeObject;

    void Start()
    {
        CreatePathLine();
    }

    void CreatePathLine()
    {
        pipeObject = new GameObject("PivotPath");
        // 親を設定しない（ワールド座標で固定）

        SpriteRenderer sr = pipeObject.AddComponent<SpriteRenderer>();

        if (pipeSprite != null)
        {
            sr.sprite = pipeSprite;
        }
        else
        {
            sr.sprite = CreateGearChainSprite();
        }

        sr.color = pathColor;
        sr.sortingOrder = sortingOrder;

        // 位置とサイズを設定
        Vector3 center = (startPosition + endPosition) / 2f;
        float distance = Vector3.Distance(startPosition, endPosition);
        Vector3 direction = (endPosition - startPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        pipeObject.transform.position = center;
        pipeObject.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        pipeObject.transform.localScale = new Vector3(lineWidth, distance, 1);
    }

    Sprite CreateGearChainSprite()
    {
        int width = 64;
        int height = 64;
        Texture2D texture = new Texture2D(width, height);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color color = Color.clear;

                if (x > width * 0.3f && x < width * 0.7f)
                {
                    color = new Color(0.4f, 0.4f, 0.45f);
                }

                int segment = (y / 8) % 2;
                if (segment == 0 && (x < width * 0.2f || x > width * 0.8f))
                {
                    color = new Color(0.3f, 0.3f, 0.35f);
                }

                if (x == (int)(width * 0.3f) || x == (int)(width * 0.7f))
                {
                    color = new Color(0.6f, 0.6f, 0.65f);
                }

                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100);
    }
}