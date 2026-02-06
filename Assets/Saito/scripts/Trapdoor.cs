using UnityEngine;
using System.Collections;

public class Trapdoor : MonoBehaviour
{
    [SerializeField] private float respawnTime = 3f; // 戻ってくるまでの時間

    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    private bool isDisappeared = false;
    private Vector2 colliderSize; // Colliderのサイズを保存

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        // Colliderのサイズを最初に保存しておく
        colliderSize = col.bounds.size;
    }

    // 落とし穴を消す
    public void Disappear()
    {
        if (isDisappeared) return;
        StartCoroutine(DisappearAndRespawn());
    }

    private IEnumerator DisappearAndRespawn()
    {
        isDisappeared = true;

        // 消える
        spriteRenderer.enabled = false;
        AudioManager.instance.PlaySE("WallDestroy");
        col.enabled = false;

        // 指定時間の80%待つ
        yield return new WaitForSeconds(respawnTime * 0.8f);

        // 警告エフェクト（点滅など）
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }

        // 戻る前にチェック
        CheckAndCrushObjects();

        // 完全に戻す
        spriteRenderer.enabled = true;
        AudioManager.instance.PlaySE("ResetWall");
        col.enabled = true;

        isDisappeared = false;
    }

    // 戻るときに中にいるオブジェクトを潰す
    private void CheckAndCrushObjects()
    {
        // 保存しておいたサイズを使用
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            transform.position,
            colliderSize,
            0f
        );

        Debug.Log($"検出されたオブジェクト数: {hits.Length}");

        foreach (Collider2D hit in hits)
        {
            // 自分自身は無視
            if (hit == col) continue;

            Debug.Log($"検出: {hit.gameObject.name}, タグ: {hit.tag}");

            // プレイヤーを潰す
            if (hit.CompareTag("Player"))
            {
                Playermove playerMove = hit.gameObject.GetComponent<Playermove>();
                if (playerMove != null)
                {
                    playerMove.TakeDamage(1);
                    AudioManager.instance.PlaySE("Trapdeadenemy");
                    Debug.Log("プレイヤーが潰された！");
                }
            }

            // エネミーを潰す
            if (hit.CompareTag("Enemy"))
            {
                Destroy(hit.gameObject);
                AudioManager.instance.PlaySE("Trapdeadenemy");
                Debug.Log("エネミーが潰された！");
            }
        }
    }
}