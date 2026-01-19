using UnityEngine;

public class EnemyClearObject : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies; // エネミーの配列

    private void Update()
    {
        // 全てのエネミーが死んだかチェック
        if (AreAllEnemiesDead())
        {
            // このオブジェクト自体を消す
            Destroy(gameObject);
        }
    }

    private bool AreAllEnemiesDead()
    {
        // 配列が空の場合
        if (enemies == null || enemies.Length == 0)
        {
            return false;
        }

        // 1体でも生きていたらfalse
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                return false;
            }
        }

        // 全員null（死んでいる）ならtrue
        return true;
    }
}