using UnityEngine;

public class PivotPathVisualizer : MonoBehaviour
{
    [Header("経路表示設定")]
    [SerializeField] Vector3 onPivotPosition;
    [SerializeField] Vector3 offPivotPosition;
    [SerializeField] Color pathColor = Color.gray;
    [SerializeField] float lineWidth = 0.1f;
    [SerializeField] bool showDirection = true;
    [SerializeField] int arrowCount = 3;
    [SerializeField] float arrowSize = 0.3f;
    [SerializeField] Material lineMaterial; // 線用のマテリアル
    [SerializeField] int sortingOrder = -100; // 描画順序（小さいほど後ろ）

    private LineRenderer pathLine;
    private GameObject arrowsParent;

    void Start()
    {
        CreatePathLine();
        if (showDirection)
        {
            CreateArrows();
        }
    }

    void CreatePathLine()
    {
        // LineRendererを作成
        GameObject lineObj = new GameObject("PivotPath");
        lineObj.transform.SetParent(transform);
        pathLine = lineObj.AddComponent<LineRenderer>();

        // LineRendererの設定
        pathLine.startWidth = lineWidth;
        pathLine.endWidth = lineWidth;
        pathLine.positionCount = 2;
        pathLine.SetPosition(0, offPivotPosition);
        pathLine.SetPosition(1, onPivotPosition);
        pathLine.startColor = pathColor;
        pathLine.endColor = pathColor;

        // マテリアルの設定
        if (lineMaterial != null)
        {
            pathLine.material = lineMaterial;
        }
        else
        {
            // デフォルトマテリアルを作成
            pathLine.material = new Material(Shader.Find("Sprites/Default"));
            pathLine.material.color = pathColor;
        }

        pathLine.useWorldSpace = true;
        pathLine.sortingOrder = sortingOrder;
    }

    void CreateArrows()
    {
        arrowsParent = new GameObject("Arrows");
        arrowsParent.transform.SetParent(transform);

        Vector3 direction = (onPivotPosition - offPivotPosition).normalized;
        Vector3 right = Vector3.Cross(direction, Vector3.up);
        if (right.magnitude < 0.1f)
        {
            right = Vector3.Cross(direction, Vector3.right);
        }
        right = right.normalized;

        for (int i = 1; i <= arrowCount; i++)
        {
            float t = i / (float)(arrowCount + 1);
            Vector3 arrowPos = Vector3.Lerp(offPivotPosition, onPivotPosition, t);
            CreateArrow(arrowPos, direction, right);
        }
    }

    void CreateArrow(Vector3 position, Vector3 direction, Vector3 right)
    {
        GameObject arrowObj = new GameObject("Arrow");
        arrowObj.transform.SetParent(arrowsParent.transform);

        LineRenderer arrowLine = arrowObj.AddComponent<LineRenderer>();
        arrowLine.startWidth = lineWidth * 0.7f;
        arrowLine.endWidth = lineWidth * 0.7f;
        arrowLine.positionCount = 3;

        Vector3 tip = position + direction * arrowSize * 0.5f;
        Vector3 left = position - direction * arrowSize * 0.5f + right * arrowSize * 0.3f;
        Vector3 rightPoint = position - direction * arrowSize * 0.5f - right * arrowSize * 0.3f;

        arrowLine.SetPosition(0, left);
        arrowLine.SetPosition(1, tip);
        arrowLine.SetPosition(2, rightPoint);

        arrowLine.startColor = pathColor;
        arrowLine.endColor = pathColor;

        if (lineMaterial != null)
        {
            arrowLine.material = lineMaterial;
        }
        else
        {
            arrowLine.material = new Material(Shader.Find("Sprites/Default"));
            arrowLine.material.color = pathColor;
        }

        arrowLine.useWorldSpace = true;
        arrowLine.sortingOrder = sortingOrder;
    }

    void OnValidate()
    {
        // エディタで値を変更したときに更新
        if (Application.isPlaying)
        {
            UpdatePath();
        }
    }

    void UpdatePath()
    {
        if (pathLine != null)
        {
            pathLine.SetPosition(0, offPivotPosition);
            pathLine.SetPosition(1, onPivotPosition);
            pathLine.startColor = pathColor;
            pathLine.endColor = pathColor;
            pathLine.startWidth = lineWidth;
            pathLine.endWidth = lineWidth;
        }

        if (arrowsParent != null)
        {
            Destroy(arrowsParent);
        }

        if (showDirection && Application.isPlaying)
        {
            CreateArrows();
        }
    }
}