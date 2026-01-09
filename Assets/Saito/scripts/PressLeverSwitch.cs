using UnityEngine;
using System.Collections;
public class LeverSwitch : MonoBehaviour
{
    [SerializeField] PressMachine targetPressMachine;

    [Header("連打防止")]
    [SerializeField] float coolTime = 0.3f;

    bool isPlayerNear = false;
    bool canInteract = true;

    void Update()
    {
        if (targetPressMachine == null) return;

        // GetKeyDown で入力
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && canInteract)
        {
            StartCoroutine(Interact());
        }
    }

    IEnumerator Interact()
    {
        canInteract = false;

        // プレス機のON/OFF
        targetPressMachine.Toggle();

        FlipLever();

        yield return new WaitForSeconds(coolTime);

        canInteract = true;
    }

    // Colliderに入ったら
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    // Colliderから出たら
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    void FlipLever()
    {
        Vector3 sc = transform.localScale;
        sc.x *= -1;
        transform.localScale = sc;
    }
}
