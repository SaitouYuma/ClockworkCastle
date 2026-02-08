using UnityEngine;

public class PivotPathVisualizer : MonoBehaviour
{
    [Header("経路表示設定")]
    [SerializeField] Transform pivotCenter;     // 回転中心点（現在位置）
    [SerializeField] Transform[] platforms;     // 3つの足場
    [SerializeField] Vector3 startCenterPosition; // 移動前の中心点位置（ワールド座標）
    [SerializeField] Vector3 endCenterPosition;   // 移動後の中心点位置（ワールド座標）

    [SerializeField] Color pathColor = Color.gray;
    [SerializeField] Color movePathColor = Color.cyan; // 移動経路の色
    [SerializeField] float lineWidth = 0.1f;
    [SerializeField] int sortingOrder = -100;

    [Header("スプライト設定")]
    [SerializeField] Sprite pipeSprite;

    private GameObject[] pipeObjects;      // 中心→足場の線
    private GameObject movePathObject;     // 移動経路の線

    void Start()
    {
        CreatePathLines();
        CreateMovePathLine();
    }

    void Update()
    {
        // 回転に追従して線を更新
        UpdatePathLines();
    }

    void CreatePathLines()
    {
        if (pivotCenter == null || platforms == null || platforms.Length == 0)
        {
            Debug.LogWarning("PivotCenter または Platforms が設定されていません");
            return;
        }

        pipeObjects = new GameObject[platforms.Length];

        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i] == null) continue;

            pipeObjects[i] = new GameObject($"PivotPath_{i}");

            SpriteRenderer sr = pipeObjects[i].AddComponent<SpriteRenderer>();
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
        }

        UpdatePathLines();
    }

    void CreateMovePathLine()
    {
        movePathObject = new GameObject("MovePath");

        SpriteRenderer sr = movePathObject.AddComponent<SpriteRenderer>();
        if (pipeSprite != null)
        {
            sr.sprite = pipeSprite;
        }
        else
        {
            sr.sprite = CreateGearChainSprite();
        }
        sr.color = movePathColor;
        sr.sortingOrder = sortingOrder - 1; // 背面に表示

        // 移動経路の線を設定
        Vector3 center = (startCenterPosition + endCenterPosition) / 2f;
        float distance = Vector3.Distance(startCenterPosition, endCenterPosition);
        Vector3 direction = (endCenterPosition - startCenterPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        movePathObject.transform.position = center;
        movePathObject.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        movePathObject.transform.localScale = new Vector3(lineWidth, distance, 1);
    }

    void UpdatePathLines()
    {
        if (pivotCenter == null || pipeObjects == null) return;

        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i] == null || pipeObjects[i] == null) continue;

            Vector3 startPos = pivotCenter.position;
            Vector3 endPos = platforms[i].position;

            // 中心点を計算
            Vector3 center = (startPos + endPos) / 2f;
            float distance = Vector3.Distance(startPos, endPos);
            Vector3 direction = (endPos - startPos).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 位置・回転・スケールを設定
            pipeObjects[i].transform.position = center;
            pipeObjects[i].transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            pipeObjects[i].transform.localScale = new Vector3(lineWidth, distance, 1);
        }
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

    void OnDestroy()
    {
        // メモリリーク防止
        if (pipeObjects != null)
        {
            foreach (var obj in pipeObjects)
            {
                if (obj != null) Destroy(obj);
            }
        }

        if (movePathObject != null)
        {
            Destroy(movePathObject);
        }
    }
}