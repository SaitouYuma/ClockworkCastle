using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;

    private Transform _Tr;
    private float direction = 1f; // 1 = 右, -1 = 左

    private void Start()
    {
        _Tr = transform;
    }

    private void Update()
    {
        // 常に一定方向に移動
        _Tr.position += Vector3.right * direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレスマシンに当たったら死ぬ
        if (collision.collider.CompareTag("Press"))
        {
            AudioManager.instance.PlaySE("EnemyDead");
            Destroy(gameObject);
            return;
        }

        // 壁や障害物にぶつかったら方向転換
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Ground"))
        {
            // 横方向の衝突かチェック（上下の衝突は無視）
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 法線が横向き（左右）の場合のみ折り返す
                if (Mathf.Abs(contact.normal.x) > 0.5f)
                {
                    Vector3 scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                    direction *= -1f;
                    break;
                }
            }
        }
    }
}