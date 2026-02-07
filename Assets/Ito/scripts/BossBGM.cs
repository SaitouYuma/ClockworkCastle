using System.Collections;
using UnityEngine;

public class BossBGM : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        AudioManager.instance.PlayBGM("Boss");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        AudioManager.instance.PlayBGM("IngameScene");
    }
}
