using System.Collections;
using UnityEngine;

public class BossBGM : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.instance.StopBGM();
        StartCoroutine(BossArea());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        AudioManager.instance.StopBGM();
        StartCoroutine(ExitBossArea());
    }
    IEnumerator BossArea()
    {
        AudioManager.instance.PlayBGM("Boss");
        yield return new WaitForSeconds(1f);
    }
    IEnumerator ExitBossArea()
    {
        AudioManager.instance.PlayBGM("IngameScene");
        yield return new WaitForSeconds(1f);
    }
}
