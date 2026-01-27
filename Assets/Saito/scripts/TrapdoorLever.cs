using UnityEngine;
using System.Collections;

public class TrapdoorLever : MonoBehaviour
{
    [SerializeField] private Trapdoor targetTrapdoor;

    [Header("連打防止")]
    [SerializeField] private float coolTime = 0.3f;

    private bool isPlayerNear = false;
    private bool canInteract = true;

    private void Update()
    {
        if (targetTrapdoor == null) return;

        // Fキーで操作
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && canInteract)
        {
            StartCoroutine(Interact());
        }
    }

    private IEnumerator Interact()
    {
        canInteract = false;

        // 落とし穴を消す
        targetTrapdoor.Disappear();

        // レバーの向きを変える
        AudioManager.instance.PlaySE("Lever");
        FlipLever();

        yield return new WaitForSeconds(coolTime);
        canInteract = true;
    }

    // プレイヤーが近づいた
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    // プレイヤーが離れた
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    // レバーの向きを反転
    private void FlipLever()
    {
        Vector3 sc = transform.localScale;
        sc.x *= -1;
        transform.localScale = sc;
    }
}