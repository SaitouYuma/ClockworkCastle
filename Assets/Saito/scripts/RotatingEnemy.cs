using UnityEngine;

public class RotatingEnemy : MonoBehaviour
{
    [Header("回転設定")]
    [SerializeField] private Transform centerPoint; // 中心点
    [SerializeField] private float rotationSpeed = 50f; // 回転速度（度/秒）
    [SerializeField] private float radius = 5f; // 回転半径

    [Header("オプション")]
    [SerializeField] private bool clockwise = true; // 時計回りかどうか
    [SerializeField] private float startAngle = 0f; // 開始角度

    private float currentAngle;

    void Start()
    {
        currentAngle = startAngle;

        // 中心点が設定されていない場合、親オブジェクトを中心点とする
        if (centerPoint == null && transform.parent != null)
        {
            centerPoint = transform.parent;
        }

        // 初期位置を設定
        UpdatePosition();
    }

    void Update()
    {
        // 角度を更新
        float direction = clockwise ? -1f : 1f;
        currentAngle += rotationSpeed * direction * Time.deltaTime;

        // 角度を0-360の範囲に正規化
        currentAngle = Mathf.Repeat(currentAngle, 360f);

        // 位置を更新
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (centerPoint == null)
        {
            Debug.LogWarning("中心点が設定されていません");
            return;
        }

        // ラジアンに変換
        float angleInRadians = currentAngle * Mathf.Deg2Rad;

        // 2D空間での位置計算
        float x = centerPoint.position.x + Mathf.Cos(angleInRadians) * radius;
        float y = centerPoint.position.y + Mathf.Sin(angleInRadians) * radius;

        transform.position = new Vector3(x, y, transform.position.z);
    }

    // エディタでギズモを表示（開発時のデバッグ用）
    private void OnDrawGizmos()
    {
        if (centerPoint == null) return;

        // 回転軌道を表示
        Gizmos.color = Color.yellow;
        DrawCircle2D(centerPoint.position, radius, 50);

        // 中心点から現在位置への線を表示
        Gizmos.color = Color.red;
        Gizmos.DrawLine(centerPoint.position, transform.position);
    }

    private void DrawCircle2D(Vector3 center, float r, int segments)
    {
        float angleStep = 360f / segments;
        Vector3 prevPoint = center + new Vector3(r, 0, 0);

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 newPoint = center + new Vector3(
                Mathf.Cos(angle) * r,
                Mathf.Sin(angle) * r,
                0
            );
            Gizmos.DrawLine(prevPoint, newPoint);
            prevPoint = newPoint;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Press"))
        {
            AudioManager.instance.PlaySE("EnemyDead");
            Destroy(gameObject);
            return;
        }
    }
}